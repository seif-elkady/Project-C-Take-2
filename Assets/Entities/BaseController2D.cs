
using System;
using UnityEngine;


public class BaseController2D : MonoBehaviour
{
    [SerializeField] private Transform _groundCheck;
    [SerializeField] protected float _acceleration;


    [SerializeField] protected float speed;
    [SerializeField] protected float jumpHeight;
    [SerializeField] protected int totalJumps;
    [SerializeField] protected float checkRadius;
    [SerializeField] protected LayerMask groundMask;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform body;
    protected int jumpsRemaining;
    protected float bodyDefaultScale;

    protected virtual void Start()
    {
        bodyDefaultScale = body.transform.localScale.x;
    }

    protected virtual void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            if (IsGrounded())
            {
                jumpsRemaining = totalJumps;
                // TODO: Play landing sound
            }
        }
    }
    public virtual void Move(float inputX)
    {
        var finalX = Mathf.MoveTowards(rb.velocity.x, inputX * speed, _acceleration * Time.deltaTime);
        rb.velocity = new Vector2(finalX, rb.velocity.y);
        SpriteFlippingHandler(inputX);
    }
    public virtual void Jump()
    {
        if (jumpsRemaining > 0)
        {
            //AudioManager.Instance.Play("PlayerJump");
            jumpsRemaining--;
            rb.velocity = new Vector2(0, jumpHeight);
        }
    }
    public bool IsGrounded() 
    {
        return Physics2D.OverlapCircle(_groundCheck.position, checkRadius, groundMask);
    }
   

    protected virtual void SpriteFlippingHandler(float x)
    {
        if (x > 0)
            body.transform.localScale = new Vector2(bodyDefaultScale, body.transform.localScale.y);

        else if (x < 0)
            body.transform.localScale = new Vector2(-bodyDefaultScale, body.transform.localScale.y);
    }

}

