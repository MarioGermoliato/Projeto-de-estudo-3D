using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteracaoRaycast : MonoBehaviour
{

    public Camera camera;
    public float distanceToInteract = 1f;
    public Text itemText;

    
    private void Update()
    {
        Interact();
    }

    void Interact()
    {

        RaycastHit hitInfo;
        

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hitInfo, distanceToInteract, LayerMask.GetMask("Interactable")))
        {
            if(hitInfo.collider.CompareTag("Colectable"))
            {
                itemText.text = "Coletar item";
            }
            else if(hitInfo.collider.CompareTag("Door"))
            {
                itemText.text = "Abrir porta";
            }
            else if(hitInfo.collider.CompareTag("NumberCode"))
            {
                itemText.text = "Inserir senha";
            }
            else if(hitInfo.collider.CompareTag("Gerador"))
            {
                itemText.text = "Usar gerador";
            }
            else if(hitInfo.collider.CompareTag("Phone"))
            {
                itemText.text = "Usar telefone";
            }
            else if(hitInfo.collider.CompareTag("Ship"))
            {
                itemText.text = "Entrar no barco";
            }
            else
            {
                itemText.text = "";
            }


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                IInteractable obj = hitInfo.transform.GetComponent<IInteractable>();

                if (obj == null) return;

                obj.Interact();

            }
        }
        else
        {
            itemText.text = "";
        }
    }
}
