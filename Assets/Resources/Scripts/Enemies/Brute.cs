using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brute : Enemy
{
    /// <summary>
    /// Melee damage the brute currently deals
    /// </summary>
    private float meleeDamage = 2.0f;

    // Start is called before the first frame update
    protected new void Start()
    {
        health = 5.0f;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Overwritten movement method for the brute enemy type. Moves to where the player is located at the current frame
    /// </summary>
    /// <param name="player">The Player the brute is chasing after</param>
    public override void Movement(Player player)
    {
        directionToMove = player.transform.position - transform.position;

        rb.AddForce(directionToMove.normalized, ForceMode.Acceleration);
    }

    protected override void AttackPlayer(Player player)
    {
        player.Health -= meleeDamage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AttackPlayer(collision.gameObject.GetComponent<Player>());
        }
        
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= collision.gameObject.GetComponent<Bullet>().Damage;
        }
    }
}
