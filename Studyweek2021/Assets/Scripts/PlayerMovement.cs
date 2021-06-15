using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public int MaxJumps;
    public int JumpCounter;
    private float xInput;
    private bool isLookingRight = true;
    public Rigidbody2D RB;
    public Vector2 CheckBox;
    public LayerMask GroundLayer;
    public Transform FeetTrans;
    Animator anim;
    private SpriteRenderer sprite;

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        JumpCounter = MaxJumps;

        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        GroundCheck();

        if (Input.GetKeyDown(KeyCode.Space) && JumpCounter > 0)
        {
            RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            JumpCounter--;

            Animations();
        }
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(xInput * Speed, RB.velocity.y);
    }

    void GroundCheck()
    {
        Collider2D checkbox = Physics2D.OverlapBox(FeetTrans.position, CheckBox, 1, GroundLayer);
        
        if (checkbox)
        {
            JumpCounter = MaxJumps;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(FeetTrans.position, CheckBox);
    }

    private void Animations()
    {
        //anim.SetFloat("Speed", Mathf.Abs(xInput));

        if (xInput < 0 && isLookingRight)
        {
            sprite.flipX = true;
            isLookingRight = false;
        }
        else if(xInput > 0 && !isLookingRight)
        {
            sprite.flipX = false;
            isLookingRight = true;
        }
    }

}
