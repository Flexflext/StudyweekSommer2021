using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Transform bulletSpawn;
    public Transform player;

    public Rigidbody2D bulletPrefab;
    Rigidbody2D clone;

    public float bulletSpeed = 800f;

    void Start()
    {
        bulletSpawn = GameObject.Find("BulletSpawn").transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Feuer Frei!");
            Attack();
        }
    }

    void Attack()
    {
        if(bulletSpawn.position.x > player.position.x)
        {
            clone = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            clone.AddForce(bulletSpawn.transform.right * bulletSpeed);
        }
        else
        {
            clone = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            clone.AddForce(bulletSpawn.transform.right * -bulletSpeed);
        }
    }
}
