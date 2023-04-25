
using UnityEngine;


public class FlyingProjectile : MonoBehaviour, IFlyingProjectile
{
  
    [HideInInspector] public LayerMask targetMask;

    [SerializeField] private DirectionMode _mode;
    [SerializeField] private Rigidbody2D _rb;

    private FlyingProjectileScriptable _projectileData;
    private Vector2 _direction;
    private bool _shouldMove;

    private void Start()
    {
        _shouldMove = true;
        _projectileData = AssetManager.instance.fireballStats;

        _direction = -transform.right;
        _rb.velocity = new Vector2(_direction.x, _direction.y).normalized * _projectileData.speed;

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
            DamageInfo info = new DamageInfo(_projectileData.baseDamage, _projectileData.damageType);
            targetHit.GetComponent<DamageSystem>().TakeDamage(info);
            // TODO: Play explosion effect

            if (_projectileData.onDestroyPrefab != null)
            {
                Instantiate(_projectileData.onDestroyPrefab, transform.position, _projectileData.onDestroyPrefab.transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    public static bool IsSameMask(int collisionMask, LayerMask targetMask)
    {
        return ((1 << collisionMask) & targetMask) != 0;
    }

    public void Move()
    {
        
    }
    public void Setup(LayerMask targetMask, FlyingProjectileScriptable stats)
    {
        _projectileData = stats;
        this.targetMask = targetMask;
    }
    enum DirectionMode
    {
        FollowMouse,
        Predefined
    }
}

