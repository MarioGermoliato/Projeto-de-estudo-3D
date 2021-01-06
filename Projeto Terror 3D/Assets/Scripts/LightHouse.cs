using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightHouse : MonoBehaviour, IInteractable
{
    private Inventory _inventory;
    public bool lightOn;
    public GameObject light;
    public Text itemText;
    public GameObject itemText2;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = FindObjectOfType(typeof(Inventory)) as Inventory;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        if (lightOn == false)
        {
            foreach (GameObject item in _inventory.inventory)
            {
                if (item.name == "Gasolina")
                {
                    lightOn = true;
                    light.SetActive(true);
                    _inventory.inventory.Remove(item);
                    break;
                }
              
            }

            if (lightOn == false)
            {
                itemText2.SetActive(false);
                itemText.text = "Você precisa de gasolina";
                StartCoroutine("EraseText");
            }
            else
            {
                itemText2.SetActive(false);
                itemText.text = "Você ligou o gerador com sucesso";
                StartCoroutine("EraseText");
            }
        }
        else
        {
            itemText2.SetActive(false);
            itemText.text = "Você já ativou o gerador";
            StartCoroutine("EraseText");
        }

    }

    IEnumerator EraseText()
    {
        yield return new WaitForSeconds(3f);
        itemText.text = " ";
        itemText2.SetActive(true);
    }
}
