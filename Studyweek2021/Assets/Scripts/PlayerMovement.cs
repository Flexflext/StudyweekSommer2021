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
    private SpriteRenderer[] sprite;
    public UnityEvent OnLandEvent;

    [SerializeField] private Transform rotaterObj;

    [SerializeField] private ParticleSystem fallOnGroundVfx;
    [SerializeField] private ParticleSystem jetPackVfx;


    private bool wasGrounded;

    private Animator[] animators;


    void Start()
    {
        animators = GetComponentsInChildren<Animator>();
        RB = GetComponent<Rigidbody2D>();

        if(OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        } 

        JumpCounter = MaxJumps;

        anim = GetComponent<Animator>();
        sprite = GetComponentsInChildren<SpriteRenderer>();
    }
    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(xInput) > 0)
        {
            foreach (var item in animators)
            {
                item.SetBool("isRunning", true);
            }
        }
        else
        {
            foreach (var item in animators)
            {
                item.SetBool("isRunning", false);
            }
        }


        GroundCheck();

        if (Input.GetKeyDown(KeyCode.Space) && JumpCounter > 0)
        {
            JumpCounter--;

            if (MaxJumps > 1 && JumpCounter == 0)
            {
                foreach (var item in animators)
                {
                    item.Play("HDSuperJump");
                    jetPackVfx.Play();
                    item.SetBool("isJumping", true);
                }
            }
            else
            {
                foreach (var item in animators)
                {
                    item.SetBool("isJumping", true);
                }
            }
            
            RB.velocity = new Vector2(RB.velocity.x, 0);
            RB.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            //anim.SetBool("IsJumping", true);
        }

        Animations();
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(xInput * Speed, RB.velocity.y);
    }

    void GroundCheck()
    {
        if (RB.velocity.y > 0)
        {
            return;
        }

        Collider2D checkBox = Physics2D.OverlapBox(FeetTrans.position, CheckBox, 0, GroundLayer);
        
        if (checkBox)
        {
            if (!wasGrounded)
            {
                wasGrounded = true;
                fallOnGroundVfx.Emit(30);

                //Landing
            }

            JumpCounter = MaxJumps;

            foreach (var item in animators)
            {
                item.SetBool("isJumping", false);
            }
        }
        else
        {
            wasGrounded = false;

            foreach (var item in animators)
            {
                item.SetBool("isJumping", true);
            }
        }

        //if (!checkBox)
        //{
        //    OnLandEvent.Invoke();
        //}
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
            foreach (var item in sprite)
            {
                item.flipX = true;
            }


            rotaterObj.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            isLookingRight = false;
        }
        else if(xInput > 0 && !isLookingRight)
        {
            foreach (var item in sprite)
            {
                item.flipX = false;
            }
            rotaterObj.rotation = Quaternion.Euler(new Vector3(0, 0f, 0));
            isLookingRight = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(FeetTrans.position, CheckBox);
    }
}
