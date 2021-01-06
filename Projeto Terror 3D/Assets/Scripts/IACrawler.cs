using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum CrawlerState
{
    Waiting, Patrol, Warning, Attack, Kill
}

public class IACrawler : MonoBehaviour
{
    private GameController _gameController;
    public GameObject defeatPanel;
    public Text defeatText;

    public Transform target; // jogador
    public Vector3 destination; //destino do navmesh
    public NavMeshAgent agent;
    public bool newDestinationNeeded;
    public int indice;

    public Transform[] patrolDestinations; //locais de destino da patrulha
    public float distance; //distancia entre o player e o crawler
    public float safeDistance; // distancia segura para se aproximar pelas costas

    public float escapeDistance; //distancia para escapar da criatura
    public float distanceToAttack;
    public float distanceToKill;


    private Animator crawlerAnimator;

    public CrawlerState crawlerCurrentState;


    [Header("Raycast")]
    public string tagDosInimigos = "Respawn";
    [Range(2, 180)]
    public float raiosExtraPorCamada = 20;
    [Range(5, 180)]
    public float anguloDeVisao = 120;
    [Range(1, 10)]
    public int numeroDeCamadas = 3;
    [Range(0.02f, 0.15f)]
    public float distanciaEntreCamadas = 0.1f;
    public float distanciaDeVisao = 10;
    public Transform cabecaInimigo;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        StartCoroutine("StartPatrol");
        agent = GetComponent<NavMeshAgent>();
        crawlerAnimator = GetComponent<Animator>();

        destination = patrolDestinations[Random.Range(0, patrolDestinations.Length)].position;
        //destination = target.position;

    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = destination;
        CrawlerMovement();
        DistanceCalculation();
        
        if(crawlerCurrentState == CrawlerState.Patrol)
        {
            EnemyCheck();
        }    

       if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(agent.remainingDistance);
        }

    }

    public void CrawlerChangeStates(CrawlerState newCrawlerState)
    {
        crawlerCurrentState = newCrawlerState;
    }

    public void CrawlerMovement()
    {
        if (crawlerCurrentState == CrawlerState.Waiting)
        {
            agent.speed = 0;
            crawlerAnimator.SetInteger("crawlerInt", 1);
        }
        else if (crawlerCurrentState == CrawlerState.Patrol)
        {
            agent.speed = 3.5f;
            crawlerAnimator.SetInteger("crawlerInt", 2);
            if (agent.remainingDistance <= 0 && newDestinationNeeded == false)
            {
                Debug.Log("2");
                newDestinationNeeded = true;
                Debug.Log("3");
                StartCoroutine("NewDestinationControl");
                destination = patrolDestinations[indice].position;
                agent.destination = destination;

            }

            if (distance < safeDistance)
            {
                CrawlerChangeStates(CrawlerState.Warning);
            }

        }
        else if (crawlerCurrentState == CrawlerState.Warning)
        {
            agent.speed = 5f;
            crawlerAnimator.SetInteger("crawlerInt", 2);
            destination = target.position;
            Debug.Log("Player avistado");

            if(agent.remainingDistance >= escapeDistance)
            {
                CrawlerChangeStates(CrawlerState.Patrol);
            }

            if(agent.remainingDistance < distanceToAttack)
            {
                CrawlerChangeStates(CrawlerState.Attack);
            }
        }
        else if(crawlerCurrentState == CrawlerState.Attack)
        {
            agent.speed = 8f;
            crawlerAnimator.SetInteger("crawlerInt", 3);
            destination = target.position;

            if (agent.remainingDistance >= escapeDistance)
            {
                CrawlerChangeStates(CrawlerState.Patrol);
            }

            if (agent.remainingDistance < distanceToKill && distance < escapeDistance)
            {
                CrawlerChangeStates(CrawlerState.Kill);
            }
        }
        else if(crawlerCurrentState == CrawlerState.Kill)
        {
            if (agent.remainingDistance < distanceToKill)
            {
                agent.speed = 10f;
                crawlerAnimator.SetInteger("crawlerInt", 4);
                destination = target.position;
            }
            else
            {
                CrawlerChangeStates(CrawlerState.Attack);
            }
        }
    }

    public void DistanceCalculation()
    {
        distance = Vector3.Distance(agent.transform.position, target.position);
    }




    private void EnemyCheck()
    {
        
            float limiteCamadas = numeroDeCamadas * 0.5f;
            for (int x = 0; x <= raiosExtraPorCamada; x++)
            {
                for (float y = -limiteCamadas + 0.5f; y <= limiteCamadas; y++)
                {
                    float angleToRay = x * (anguloDeVisao / raiosExtraPorCamada) + ((180.0f - anguloDeVisao) * 0.5f);
                    Vector3 directionMultipl = (-cabecaInimigo.right) + (cabecaInimigo.up * y * distanciaEntreCamadas);
                    Vector3 rayDirection = Quaternion.AngleAxis(angleToRay, cabecaInimigo.up) * directionMultipl;
                    //
                    RaycastHit hitRaycast;
                    if (Physics.Raycast(cabecaInimigo.position, rayDirection, out hitRaycast, distanciaDeVisao))
                    {
                        if (!hitRaycast.transform.IsChildOf(transform.root) && !hitRaycast.collider.isTrigger)
                        {
                            if (hitRaycast.collider.gameObject.CompareTag(tagDosInimigos))
                            {
                                Debug.DrawLine(cabecaInimigo.position, hitRaycast.point, Color.red);
                                CrawlerChangeStates(CrawlerState.Warning);
                                
                            }
                        }
                    }
                    else
                    {
                        Debug.DrawRay(cabecaInimigo.position, rayDirection * distanciaDeVisao, Color.green);
                    }
                }
            }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagDosInimigos))
        {
            defeatPanel.SetActive(true);
            defeatText.text = "Você morreu";
            _gameController.ChangeState(GameState.Inventory);
        }
    }

    IEnumerator NewDestinationControl()
    {
        indice = Random.Range(0, patrolDestinations.Length);
        yield return new WaitForSeconds(3);
        newDestinationNeeded = false;

    }

    IEnumerator StartPatrol()
    {
        yield return new WaitForSeconds(5f);
        crawlerCurrentState = CrawlerState.Patrol;
    }
}
