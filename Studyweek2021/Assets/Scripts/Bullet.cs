using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();

            enemy.TakeDmg(Damage);
        }

        Destroy(this.gameObject);
    }
}
