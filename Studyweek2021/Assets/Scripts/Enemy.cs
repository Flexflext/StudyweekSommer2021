using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius;

    [Header("Refs")]
    [SerializeField] private GameObject gfx;  
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    private Collider2D enemyCollider;
    private SpriteRenderer spRend;

    private Vector3 startPos;
    private bool isDead;

    private void Start()
    {
        startPos = transform.position;
        spRend = GetComponentInChildren<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!isDead)
        {
            ChangeDir();
            transform.position += (new Vector3(speed, 0, 0) * Time.deltaTime);
        }   
    }

    private void ChangeDir()
    {
        bool changeDir = false;

        if (IsOnLedge())
        {
            changeDir = true;
        }
        if (IsAtWall())
        {
            changeDir = true;
        }

        if (changeDir)
        {
            spRend.flipX = !spRend.flipX;

            groundCheck.localPosition = new Vector3(groundCheck.localPosition.x * -1f, groundCheck.localPosition.y, groundCheck.localPosition.z);
            wallCheck.localPosition = new Vector3(wallCheck.localPosition.x * -1f, wallCheck.localPosition.y, wallCheck.localPosition.z);

            speed *= -1f;
        }
    }

    private bool IsOnLedge()
    {
        bool ground = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        return !ground;
    }

    private bool IsAtWall()
    {
        bool wall = Physics2D.OverlapCircle(wallCheck.position, checkRadius, groundLayer);

        return wall;
    }

    private IEnumerator RespawnCounter(float _time)
    {
        yield return new WaitForSeconds(_time);

        isDead = false;
        transform.position = startPos;
        gfx.gameObject.SetActive(true);
        enemyCollider.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
    }
}
