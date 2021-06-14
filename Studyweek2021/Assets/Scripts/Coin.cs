using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float timeTillRespawn;
    [SerializeField] private float magnetizeSpeed;

    [Header("Refs")]
    [SerializeField] private GameObject gfx;
    private Collider2D circleCollider;

    private Vector3 startPos;

    public bool magnetizeToPlayer;

    private Vector3 playerPos = Vector3.zero;

    private void Start()
    {
        circleCollider = GetComponent<Collider2D>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (magnetizeToPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, playerPos, magnetizeSpeed * Time.deltaTime);
        }
    }


    private IEnumerator RespawnCounter(float _time)
    {
        yield return new WaitForSeconds(_time);

        transform.position = startPos;
        gfx.gameObject.SetActive(true);
        circleCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            magnetizeToPlayer = false;
            gfx.gameObject.SetActive(false);
            circleCollider.enabled = false;
            StartCoroutine(RespawnCounter(timeTillRespawn));
        }
    }
}
