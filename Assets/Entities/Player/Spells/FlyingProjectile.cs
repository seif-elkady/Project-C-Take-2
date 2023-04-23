
using System;
using UnityEngine;


public class FlyingProjectile : MonoBehaviour, IFlyingProjectile
{
  
    [HideInInspector] public LayerMask targetMask;

    [SerializeField] private DirectionMode _mode;
    [SerializeField] private Rigidbody2D _rb;

    private FlyingProjectileScriptable _stats;
    private Camera _camera;
    private Vector2 _direction;
    private Vector3 _rotation;
    private bool _shouldMove;

    private void Start()
    {
        _camera = UiManager.instance.mainCamera;
        _shouldMove = true;
        _stats = AssetManager.instance.fireballStats;

        _direction = -transform.right;
        _rb.velocity = new Vector2(_direction.x, _direction.y).normalized * _stats.speed;

        Destroy(gameObject, 6f);
    }

    public void Update()
    {
        if (!_shouldMove)
            _rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var targetHit = collision.gameObject;
        if (IsSameMask(targetHit.layer, targetMask))
        {
            Destroy(gameObject);
            DamageInfo info = new DamageInfo(_stats.baseDamage, DamageTypesSystem.DamageTypes.Fire);
            targetHit.GetComponent<DamageSystem>().TakeDamage(info);
            // TODO: Play explosion effect
        }
    }

    public static bool IsSameMask(int collisionMask, LayerMask targetMask)
    {
        return ((1 << collisionMask) & targetMask) != 0;
    }

    public void Move()
    {
        
    }

    enum DirectionMode
    {
        FollowMouse,
        Predefined
    }
}

