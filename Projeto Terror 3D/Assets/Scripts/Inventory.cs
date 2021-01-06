using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private GameController _gameController;
    public List<GameObject> inventory;
    public GameObject inventoryWindow;
    public bool isInventoryOpen;
    public Image[] slotInventory;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        inventoryWindow.SetActive(isInventoryOpen);   
    }

    // Update is called once per frame
    void Update()
    {
        LoadInventory();
    }

    void LoadInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) && _gameController.currentState != GameState.ItemInfo)
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryWindow.SetActive(isInventoryOpen);

            switch (isInventoryOpen)
            {
                case true:
                    _gameController.ChangeState(GameState.Inventory);
                    int i = 0;
                    foreach(GameObject item in inventory)
                    {
                        slotInventory[i].sprite = item.GetComponent<Colectables>().itemImage;
                        slotInventory[i].GetComponent<BagSlot>().itemSlot = item.GetComponent<Colectables>();
                        i++;
                    }
                    break;

                case false:
                    _gameController.ChangeState(GameState.Gameplay);
                    foreach(Image img in slotInventory)
                    {
                        img.sprite = null;
                    }
                    break;

            }
        }
    }

    public void InventoryUpdate()
    {
        foreach (Image img in slotInventory)
        {
            img.sprite = null;
            img.GetComponent<BagSlot>().itemSlot = null;
        }

        int i = 0;
        foreach (GameObject item in inventory)
        {
            slotInventory[i].sprite = item.GetComponent<Colectables>().itemImage;
            slotInventory[i].GetComponent<BagSlot>().itemSlot = item.GetComponent<Colectables>();
            i++;
        }

    }
}
