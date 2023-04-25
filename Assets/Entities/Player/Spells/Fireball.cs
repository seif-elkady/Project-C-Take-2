
using UnityEngine;


public class Fireball : MonoBehaviour , ISpell
{
    private FlyingProjectileScriptable _fireballStats;
    private float _cooldown;
    private float _currentCooldown;
    private void Start()
    {
        _fireballStats = AssetManager.instance.fireballStats;
        _cooldown = _fireballStats.cooldown;
    }
    private void Update()
    {
        if (_currentCooldown > 0)
            _currentCooldown -= Time.deltaTime;
    }
    public void Cast(Transform spawnLocation, LayerMask targetMask)
    {
        var fireball = Instantiate(AssetManager.instance.fireballPrefab, spawnLocation.position, spawnLocation.parent.rotation);
        fireball.GetComponent<FlyingProjectile>().Setup(targetMask, _fireballStats);
        _currentCooldown = _cooldown;
        
    }  

    public bool IsReady()
    {
        return _currentCooldown <= 0;
    }

    public void SetMaxCooldown(float newCooldown)
    {
        _cooldown = newCooldown;
    }
}

