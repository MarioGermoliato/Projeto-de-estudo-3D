using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagSlot : MonoBehaviour, IPointerDownHandler
{
    public Colectables itemSlot;
    public GameObject painelInfo;
    private GameController _gameController;
    private PanelItemInfo itemPanel;
    // Start is called before the first frame update
    private void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(itemSlot != null)
        {
            itemPanel = painelInfo.GetComponent<PanelItemInfo>();
            //itemSlot.UseItem();
            _gameController.currentState = GameState.ItemInfo;
            itemPanel.nameItem.text = itemSlot.itemName;
            itemPanel.descriptionItem.text = itemSlot.itemDescription;
            itemPanel.spriteItem.sprite = itemSlot.itemImage;
            itemPanel.Colectables = itemSlot;

            if(itemSlot.consumable == false)
            {
                painelInfo.GetComponent<PanelItemInfo>().btnUse.SetActive(false);
                painelInfo.GetComponent<PanelItemInfo>().btnDiscard.SetActive(false);
            }
            else
            {
                painelInfo.GetComponent<PanelItemInfo>().btnUse.SetActive(true);
                painelInfo.GetComponent<PanelItemInfo>().btnDiscard.SetActive(true);

            }





            painelInfo.SetActive(true);
        }
        else
        {
            Debug.Log("Não tem nada");
        }
    }

    

}
