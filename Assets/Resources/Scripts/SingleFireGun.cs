using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFireGun : MonoBehaviour
{
    private Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        firePoint = GameObject.Find("FirePoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Logic for the player when they press the shoot button
        if (Input.GetMouseButtonDown(0))
        {
            /*GameObject tempBullet = Instantiate(GameManager.BulletPrefab,
                new Vector3(firePoint.position.x, firePoint.position.y, firePoint.position.z),
                Quaternion.identity);*/
            bool hit = Physics.Raycast(firePoint.position, Camera.main.transform.forward, 100.0f);

            Debug.Log(hit);
        }
    }
}
