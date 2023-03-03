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
    private static GameObject bulletPrefab;

    private Player player;
    private static Camera followCamera;

    public static Camera FollowCamera
    {
        get { return followCamera; }
    }

    public static GameObject BulletPrefab
    {
        get { return bulletPrefab; }
    }

    /// <summary>
    /// Constnat for the gravity of the environment
    /// </summary>
    public const float GLOBAL_GRAVITY = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        bulletPrefab = (GameObject)Resources.Load("Prefabs/Bullet");

        player = GameObject.Find("Player").GetComponent<Player>();
        followCamera = GameObject.Find("FollowCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Setting the follow camera to follow the player
        followCamera.transform.position = player.transform.position;
    }
}
