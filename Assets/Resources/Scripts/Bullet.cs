using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Bullet rigidbody
    /// </summary>
    private Rigidbody rb;

    private float damage = 1.0f;

    private const float MAX_ALIVE_TIME = 2.5f;
    private float aliveTime;

    public float Damage 
    { 
        get { return damage; } 
    }

    // Start is called before the first frame update
    void Start()
    {
        aliveTime = MAX_ALIVE_TIME;
        rb = GetComponent<Rigidbody>();

        rb.AddForce(GameManager.FollowCamera.transform.forward.normalized * 100.0f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (aliveTime > 0.0f)
        { aliveTime -= Time.deltaTime; }
        else
        {
            if (this != null)
            { Destroy(gameObject); }
        }
    }
}
