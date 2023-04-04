using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu,
    Options,
    Inventory,
    Game,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameState currentGameState;
    private static GameObject bulletPrefab;
    private Canvas inventoryCanvas;

    private Player player;
    private static Camera followCamera;

    private List<Enemy> enemies = new List<Enemy>();

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

        inventoryCanvas = GameObject.Find("Inventory").GetComponent<Canvas>();
        inventoryCanvas.gameObject.SetActive(false);
        player = GameObject.Find("Player").GetComponent<Player>();
        followCamera = GameObject.Find("FollowCamera").GetComponent<Camera>();

        foreach(GameObject enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(enemyObj.GetComponent<Enemy>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentGameState != GameState.Inventory)
        {
            // Setting the follow camera to follow the player
            followCamera.transform.position = player.transform.position;

            foreach (Enemy enemy in enemies)
            {
                if (enemy.Health <= 0.0f)
                {
                    enemies.Remove(enemy);
                    Destroy(enemy);
                    break;
                }

                enemy.Movement(player);
            }
        }  

        OpenInventory();
    }

    /// <summary>
    /// Helper function to handle when the player is in their inventory
    /// </summary>
    private void OpenInventory()
    {
        // Determining what state the game should be in
        if (inventoryCanvas.gameObject.activeSelf)
        {
            currentGameState = GameState.Inventory;
        }
        else
        {
            currentGameState = GameState.Game;
        }

        // Opening the inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryCanvas.gameObject.SetActive(!inventoryCanvas.gameObject.activeSelf);
        }

        // Stopping time if the inventory is open
        if (currentGameState == GameState.Inventory)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
