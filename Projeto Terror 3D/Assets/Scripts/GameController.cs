using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Gameplay, Inventory, ItemInfo
}
public class GameController : MonoBehaviour
{
    public GameState currentState;
    public GameObject cursorPersonagem;
  
    // Start is called before the first frame update
    void Start()
    {
        ChangeState(GameState.Gameplay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newGameState)
    {
        currentState = newGameState;

        if (currentState == GameState.Inventory || currentState == GameState.ItemInfo)
        {
            Time.timeScale = 0;
            cursorPersonagem.SetActive(false);
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            cursorPersonagem.SetActive(true);
            Cursor.visible = false;
        }
    }
}
