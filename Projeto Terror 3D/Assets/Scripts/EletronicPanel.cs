using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EletronicPanel : MonoBehaviour, IInteractable
{
    private GameController _gameController;
    public GameObject panelHud;

    public string code;
    private string codeInput;
    public bool open;
    public Text displayTxt;
    public GameObject interactionTxt;
    public Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        panelHud.SetActive(false);
        codeInput = "";
        displayTxt.text = codeInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Interact()
    {
        _gameController.ChangeState(GameState.Inventory);
        panelHud.SetActive(true);
        interactionTxt.SetActive(false);
    }

    public void btnNumber(string n)
    {
        codeInput += n;
        displayTxt.text = codeInput;
    }

    public void btnCancel()
    {
        _gameController.ChangeState(GameState.Gameplay);
        codeInput = "";
        displayTxt.text = codeInput;
        panelHud.SetActive(false);
        interactionTxt.SetActive(true);
    }

    public void btnConfirm()
    {
        StartCoroutine("codeConfirm");
    }

    IEnumerator codeConfirm()
    {
        if(codeInput == code)
        {
            displayTxt.text = "OK";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "OK";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "OK";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "";
            codeInput = "";
            open = true;
            panelHud.SetActive(false);
            interactionTxt.SetActive(true);
            _gameController.ChangeState(GameState.Gameplay);
            doorAnimator.SetBool("Open", open);
        }
        else
        {
            displayTxt.text = "ERROR";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "ERROR";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "ERROR";
            yield return new WaitForSecondsRealtime(0.2f);
            displayTxt.text = "";
            codeInput = "";
        }
    }
}
