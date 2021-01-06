using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ship : MonoBehaviour, IInteractable
{
    public Animator shipAnimator;
    private RadioConversation _radioConversation;
    private GameController _gameController;
    private LightHouse _lightHouse;
    public GameObject winPanel;
    public Text winText;

    // Start is called before the first frame update
    void Start()
    {
        _radioConversation = FindObjectOfType(typeof(RadioConversation)) as RadioConversation;
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _lightHouse = FindObjectOfType(typeof(LightHouse)) as LightHouse;
    }

    // Update is called once per frame
    void Update()
    {
        if(_radioConversation.finish && _lightHouse.lightOn)
        {
            shipAnimator.SetBool("Finish", true);
        }
    }

    public void Interact()
    {
        winPanel.SetActive(true);
        winText.text = "Você conseguiu escapar!";
        _gameController.ChangeState(GameState.Inventory);
    }
}
