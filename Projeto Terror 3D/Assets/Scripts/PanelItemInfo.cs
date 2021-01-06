using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelItemInfo : MonoBehaviour
{    
    public Text nameItem, descriptionItem;
    public Image spriteItem;

    public Colectables Colectables;
    private GameController _gameController;
    private Inventory _inventory;

    public GameObject btnUse, btnDiscard;
    
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _inventory = FindObjectOfType(typeof(Inventory)) as Inventory;
    }

    // Update is called once per frame
  
    public void UseItem()
    {
        Colectables.UseItem();
    }

    public void DiscardItem()
    {
        _inventory.inventory.Remove(Colectables.gameObject);
        _inventory.InventoryUpdate();
        ClosePanel();
    }

    public void ClosePanel()
    {
        _gameController.currentState = GameState.Inventory;
        this.gameObject.SetActive(false);
    }
}
