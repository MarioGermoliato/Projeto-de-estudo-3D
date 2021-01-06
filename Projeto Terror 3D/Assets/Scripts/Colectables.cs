using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colectables : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public bool consumable;
    public AudioSource audioSource;
    public AudioClip itemsound;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UseItem()
    {
        Debug.Log("Voce usou o item" + itemName);
    }
   
}
