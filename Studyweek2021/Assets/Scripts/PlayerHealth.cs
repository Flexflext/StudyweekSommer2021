using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Vector3 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
    }

    private void ResetToSpawnPos()
    {
        transform.position = spawnPos;
        //Play Hud Animation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            ResetToSpawnPos();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                spawnPos = transform.position;
            }
        }
    }
}
