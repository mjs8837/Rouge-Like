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

    /// <summary>
    /// Constnat for the gravity of the environment
    /// </summary>
    public const float GLOBAL_GRAVITY = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
