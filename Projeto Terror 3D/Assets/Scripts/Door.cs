using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public float abertura;
    private bool aberta;
    public float step;
    private float rotationZ;

    public bool keyNeeded;
    public string keyName;

    private Inventory _inventory;
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
            if(keyNeeded == true)
            {
                foreach(GameObject item in _inventory.inventory)
                {
                    if(item.name == keyName)
                    {
                    OpenDoor();
                    keyNeeded = false;
                    _inventory.inventory.Remove(item);
                    break;
                    }
                }

            }
            else
            {
            OpenDoor();
            }
        }

    void OpenDoor()
    {
        aberta = !aberta;
        StopCoroutine("OpenClose");
        StartCoroutine("OpenClose");
    }

    IEnumerator OpenClose()
    {
        yield return new WaitForEndOfFrame();
        if (aberta == true)
        {
            rotationZ -= step * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, rotationZ);
            if (rotationZ > abertura)
            {
                StartCoroutine("OpenClose");
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, abertura);
            }
        }
        else if (aberta == false)
        {
            rotationZ += step * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(0, 0, rotationZ);
            if (rotationZ < 0)
            {
                StartCoroutine("OpenClose");
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
        } 

    }
    

