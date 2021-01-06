using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioConversation : MonoBehaviour, IInteractable
{
    private GameController _gameController;
    public string[] messages;
    public float[] delay;
    private int idMessage;
    public GameObject text1;
    public Text text2;
    public bool finish;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if(finish == false)
        {
            _gameController.ChangeState(GameState.Inventory);
            StartCoroutine("RadioDialogue");
            finish = true;
        }
        else
        {
            StartCoroutine("DialogueFinished");
        }
    }

    IEnumerator RadioDialogue()
    {
        text1.SetActive(false);
        for(int i = 0; i < messages.Length; i++)
        {
            text2.text = messages[i];
            yield return new WaitForSecondsRealtime(delay[i]);
        }        
        text2.text = "";
        _gameController.ChangeState(GameState.Gameplay);
        text1.SetActive(true);
    }

    IEnumerator DialogueFinished()
    {
        text1.SetActive(false);
        text2.text = "Você já pediu ajuda pelo telefone";
        yield return new WaitForSeconds(2f);
        text2.text = "";
        text1.SetActive(true);
    }
}
