using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Options,
    Game,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameState currentGameState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
