using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float timeTillRespawn;
    [SerializeField] private float magnetizeSpeed;
    [SerializeField] private float upDownSpeed;
    private float currentUpDownSpeed;

    [Header("Refs")]
    [SerializeField] private GameObject gfxHigh;
    [SerializeField] private GameObject gfxLow;
    private Collider2D circleCollider;

    private Vector3 startPos;

    public bool magnetizeToPlayer;

    private Vector3 addPos = new Vector3(0, 1, 0);

    private void Start()
    {
        currentUpDownSpeed = upDownSpeed;
        circleCollider = GetComponent<Collider2D>();
        startPos = transform.position;
    }

    private void Update()
    {
        if (magnetizeToPlayer)
        {
            transform.position = Vector3.Lerp(transform.position, LevelManager.Instance.PlayerPosition, magnetizeSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += Vector3.up * currentUpDownSpeed * Time.deltaTime;

            if (transform.position.y >= startPos.y + .15f)
            {
                currentUpDownSpeed = upDownSpeed * -1f;
            }
            else if (transform.position.y <= startPos.y - .15f)
            {
                currentUpDownSpeed = upDownSpeed;
            }
        }
    }


    private IEnumerator RespawnCounter(float _time)
    {
        yield return new WaitForSeconds(_time);

        transform.position = startPos;
        gfxHigh.gameObject.SetActive(true);
        gfxLow.gameObject.SetActive(true);
        circleCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            magnetizeToPlayer = false;
            gfxHigh.gameObject.SetActive(false);
            gfxLow.gameObject.SetActive(true);
            circleCollider.enabled = false;
            StartCoroutine(RespawnCounter(timeTillRespawn));
        }
    }
}
