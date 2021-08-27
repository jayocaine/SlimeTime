using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCharacter : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public Animator anim;
    [HideInInspector] public SpriteRenderer sr;

    public float moveSpeed;
    public float gravity = -9.8f;

    private bool isGrounded;
    public static bool isMoving;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        MoveOnInput();
        FlipSpriteOnInput();
        ChangeAnimationFromInput();
        CalculateGravity();
        JumpOnInput();
        print(isGrounded);
    }
    void FlipSpriteOnInput() {
        if (Input.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            sr.flipX = true;
        }
    }
    void ChangeAnimationFromInput() {
        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("isRunning", true);
            anim.SetBool("isIdle", false);
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isIdle", true);
        }
    }
    void MoveOnInput() {
        rb2d.velocity = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f) * 8;
    }
    void CalculateGravity() {

        if (!Physics2D.Raycast(transform.position, Vector2.down, 0.7f, LayerMask.GetMask("Default")))
        {
            rb2d.velocity += new Vector2(0, gravity);
            isGrounded = false;
        }
        else {
            isGrounded = true;
        }
    }
    void JumpOnInput() {
        if (Input.GetKeyDown(KeyCode.Space) ) {
            rb2d.AddForce(Vector2.up * 50f, ForceMode2D.Impulse);
        }
    }
}
