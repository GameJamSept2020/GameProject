using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public int speed;
    public int jumpCount;
    private int jumpValue;
    private float dashTime;
    public float startDashTime;
    private int dashCount;
    public int dashSpeed;
    public int jumpForce;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float dashCooldownTime;
    private int direction;
    void Start()
    {
        jumpValue = jumpCount;
        dashCount = 1;
        dashTime = startDashTime;
        Debug.Log(dashCount);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2 (speed, rb.velocity.y);
        rb.gravityScale = 9.8f;
        if  (Input.GetButton("Jump") && jumpValue > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpValue = jumpValue - 1; 
                } 
        else if (Input.GetButton("Jump") && jumpValue == 0 && isGrounded == true)
                {
                    rb.velocity = Vector2.up * jumpForce;
                }
        if (isGrounded == true)
                {
                    jumpValue = jumpCount;
                }
        if (direction == 0)
                {
                   if (Input.GetButton("Dash") && dashCount == 1)
                        {
                            Debug.Log("Dash left");
                            direction = 1;
                            dashCount = dashCount - 1;
                            Invoke("DashReset", dashCooldownTime);
                        }
                }
            else 
                {
                    if (dashTime <= 0)
                        {
                            direction = 0;
                            dashTime = startDashTime;
                            rb.velocity = Vector2.zero;
                        }   
                    else  
                        {
                            dashTime -= Time.deltaTime;
                            //Debug.Log("dash cooldown: " + dashTime);
                        }
                }
        if (direction == 1)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                    rb.gravityScale = 0f;
                }
    }

    void FixedUpdate() 
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    void DashReset()
    {
        dashCount += 1;
    }
}