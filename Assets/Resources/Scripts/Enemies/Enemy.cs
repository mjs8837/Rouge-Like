using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract enemy class that all enemy types will inherit from
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    /// <summary>
    /// Enemy rigidbody
    /// </summary>
    protected Rigidbody rb;

    /// <summary>
    /// Direction for the enemy to move
    /// </summary>
    protected Vector3 directionToMove;

    /// <summary>
    /// Health of each enemy
    /// </summary>
    protected float health;

    /// <summary>
    /// Property for accessing enemy health
    /// </summary>
    public float Health
    {
        get { return health; }
    }

    // Start is called before the first frame update
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Abstract movement method that each enemy will need to implement for their specific movement
    /// </summary>
    public abstract void Movement(Player player);

    /// <summary>
    /// Abstract attack method that each enemy will need to implement for their specific attack(s)
    /// </summary>
    protected abstract void AttackPlayer(Player player);
}
