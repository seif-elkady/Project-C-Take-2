using System;
using UnityEngine;
public class PlayerController2D : BaseController2D
{
    [SerializeField] private Transform _rightWallCheck;
    [SerializeField] private Transform _leftWallCheck;
    [SerializeField] private float _wallSlideSpeed;
    [SerializeField] private float _wallJumpSpeed;
    [SerializeField] private float _wallJumpHeight;
    [SerializeField] private float _wallCheckRadius;

    private bool _isWallSliding;
    private Transform _wallCheck;


    protected override void Start()
    {
        base.Start();
        _wallCheck = _rightWallCheck;
    }
   
    protected override void FixedUpdate()
    {
        if (rb.velocity.y <= 0)
            HandleFalling();
        else
            _isWallSliding = false;

        base.FixedUpdate();
    }

    public override void Move(float inputX)
    {  
        base.Move(inputX);
    }

    private void HandleFalling()
    {
        if (IsTouchingWall())
        {
            rb.velocity = new Vector2(0, -_wallSlideSpeed);
            _isWallSliding = true;
        }
        else
            _isWallSliding = false;

        if (IsGrounded())
            _isWallSliding = false;
    }

    #region Jumping
    public override void Jump()
    {
        if (jumpsRemaining > 0 && !_isWallSliding)
        {
            if (maxSlopeAngle < slopeDownAngle)
                return;
            NormalJump();
        }
        else if (_isWallSliding) // If there are no jumps left and wall sliding
            WallJump();
    }

    private void NormalJump()
    {
        jumpsRemaining--;
        //AudioManager.Instance.Play("PlayerJump");
        _isWallSliding = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }
    private void WallJump()
    {
        jumpsRemaining = 0;
        _isWallSliding = false;
        var facingDirection = body.transform.localScale.x;
        rb.AddForce(new Vector2(-facingDirection * _wallJumpSpeed, _wallJumpHeight), ForceMode2D.Impulse);
        SpriteFlippingHandler(-facingDirection);
    }

    #endregion

    public override void SpriteFlippingHandler(float x)
    {
        if (x > 0)
        {
            body.transform.localScale = new Vector2(bodyDefaultScale, body.transform.localScale.y);
            _wallCheck = _rightWallCheck;
        }

        else if (x < 0)
        {
            body.transform.localScale = new Vector2(-bodyDefaultScale, body.transform.localScale.y);
            _wallCheck = _leftWallCheck;
        }
    }
    private bool IsTouchingWall() => Physics2D.OverlapCircle(_wallCheck.position, _wallCheckRadius, groundMask);
}

