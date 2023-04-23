
using UnityEngine;


public class Fireball : MonoBehaviour, ISpell
{
    private Transform _spawnLocation;
    private LayerMask _targetMask;

    public Fireball(Transform spawnLocation, LayerMask targetMask)
    {
        _spawnLocation = spawnLocation;
        _targetMask = targetMask;

    }
    public void Cast()
    {
        var fireball = Instantiate(AssetManager.instance.fireball, _spawnLocation.position, _spawnLocation.parent.rotation);
        fireball.GetComponent<FlyingProjectile>().targetMask = _targetMask;
    }

    
}

