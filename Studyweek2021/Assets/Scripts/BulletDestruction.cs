using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestruction : MonoBehaviour
{
    public float lifeSpan = 2.0f;

    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }

    void Update()
    {
        
    }
}
