
using UnityEngine;

public class BaseController2D : MonoBehaviour
{
    [SerializeField] private float _slopeCheckDistance;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private PhysicsMaterial2D _fullFriction;
    [SerializeField] private PhysicsMaterial2D _noFriction;
    private Vector2 _slopeNormalPerp;
    private Vector2 _colliderSize;

    private float _xInput;
    private float _slopeDownAngleOld;
    private float _slopeSideAngle;

    private bool _canWalkOnSlope;
    private bool _isOnSlope;
    private bool _isGrounded;

    [SerializeField] protected float speed;
    [SerializeField] protected float acceleration;
    [SerializeField] protected float jumpHeight;
    [SerializeField] protected int totalJumps;
    [SerializeField] protected float checkRadius;
    [SerializeField] protected float maxSlopeAngle;
    [SerializeField] protected LayerMask groundMask;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform body;
    protected int jumpsRemaining;
    protected float slopeDownAngle;
    protected float bodyDefaultScale;

    protected virtual void Start()
    {
        _colliderSize = _capsuleCollider.size;
        bodyDefaultScale = body.transform.localScale.x;
    }

    protected virtual void FixedUpdate()
    {
        _isGrounded = IsGrounded();
        if (rb.velocity.y < 0)
        {
            if (_isGrounded)
            {
                jumpsRemaining = totalJumps;
                // TODO: Play landing sound
            }
        }
    }
    public virtual void Move(float inputX)
    {
        _xInput = inputX;

        if (_isGrounded && _isOnSlope && _canWalkOnSlope)
        {
            rb.velocity = new Vector2(speed * _slopeNormalPerp.x * -_xInput, speed * _slopeNormalPerp.y * -_xInput);
        } 
        else if (!_isOnSlope)
        {
            var finalX = Mathf.MoveTowards(rb.velocity.x, _xInput * speed, acceleration * Time.deltaTime);
            rb.velocity = new Vector2(finalX, rb.velocity.y);
        }
        
        SpriteFlippingHandler(_xInput);
    }

    public void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, _colliderSize.y / 2);

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _slopeCheckDistance, groundMask);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _slopeCheckDistance, groundMask);

        if(slopeHitFront)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }

        else if (slopeHitBack)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _slopeSideAngle = 0.0f;
            _isOnSlope = false;
        }
    }
    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _slopeCheckDistance, groundMask);

        if (hit)
        {
            _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != _slopeDownAngleOld)
                _isOnSlope = true;

            _slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, _slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }
        if (slopeDownAngle > maxSlopeAngle || _slopeSideAngle > maxSlopeAngle)
            _canWalkOnSlope = false;
        else
            _canWalkOnSlope = true;

        if (_isOnSlope && _xInput <= 0.1f && _canWalkOnSlope)
            rb.sharedMaterial = _fullFriction;
        else
            rb.sharedMaterial = _noFriction;
    }

    public virtual void Jump()
    {
        if (jumpsRemaining > 0 && slopeDownAngle > maxSlopeAngle)
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
    public bool IsOnSlope()
    {
        return _isOnSlope;
    }
   

    protected virtual void SpriteFlippingHandler(float x)
    {
        if (x > 0)
            body.transform.localScale = new Vector2(bodyDefaultScale, body.transform.localScale.y);

        else if (x < 0)
            body.transform.localScale = new Vector2(-bodyDefaultScale, body.transform.localScale.y);
    }

}

