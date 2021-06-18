using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public int MaxJumps;
    public int JumpCounter;
    private float xInput;
    public bool isLookingRight = true;
    public Rigidbody2D RB;
    public Vector2 CheckBox;
    public LayerMask GroundLayer;
    public Transform FeetTrans;
    Animator anim;
    private SpriteRenderer sprite;
    public UnityEvent OnLandEvent;


    void Start()
    {
        RB = GetComponent<Rigidbody2D>();

        if(OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        } 

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
            //anim.SetBool("IsJumping", true);
        }
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(xInput * Speed, RB.velocity.y);
    }

    void GroundCheck()
    {
        Collider2D checkBox = Physics2D.OverlapBox(FeetTrans.position, CheckBox, 1, GroundLayer);
        
        if (checkBox)
        {
            JumpCounter = MaxJumps;
        }

        if (!checkBox)
        {
            OnLandEvent.Invoke();
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

    public void OnLanding()
    {
        anim.SetBool("IsLanding", false);
    }
}
