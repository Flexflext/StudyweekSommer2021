using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private float respawnTime;
    private int maxHealth;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkRadius;

    [Header("Refs")]
    [SerializeField] private GameObject gfxHigh;  
    [SerializeField] private GameObject gfxLow;  
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    private Collider2D enemyCollider;
    private SpriteRenderer[] spRend;

    private Vector3 startPos;
    private bool isDead;

    private void Start()
    {
        maxHealth = health;
        startPos = transform.position;
        spRend = GetComponentsInChildren<SpriteRenderer>();
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
            foreach (var item in spRend)
            {
                item.flipX = !item.flipX;
            }
            

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

    private IEnumerator DieAndRespawn(float _time)
    {
        //Play Animator
        // Wait time till Animation is Over
        isDead = true;
        enemyCollider.enabled = false;

        yield return new WaitForSeconds(0.05f);

        //Die
        gfxHigh.gameObject.SetActive(false);
        gfxLow.gameObject.SetActive(false);

        yield return new WaitForSeconds(_time);

        isDead = false;
        health = maxHealth;
        transform.position = startPos;
        gfxHigh.gameObject.SetActive(true);
        gfxLow.gameObject.SetActive(true);
        enemyCollider.enabled = true;
    }

    public void TakeDmg(int _dmg)
    {
        if (!isDead)
        {
            health -= _dmg;
            //Dmg Animation

            if (health != 0)
            {
                EnemyManager.Instance.SpawnRandomUpgrade(transform.position);
                StartCoroutine(DieAndRespawn(respawnTime));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(wallCheck.position, checkRadius);
    }
}
