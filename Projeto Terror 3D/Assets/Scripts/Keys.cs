using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : Colectables, IInteractable
{
    private Inventory _inventory;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = FindObjectOfType(typeof(Inventory)) as Inventory;
        this.gameObject.name = itemName;
    }


    public void Interact()
    {
        if (_inventory.inventory.Count < _inventory.slotInventory.Length)
        {
            _inventory.inventory.Add(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

}
