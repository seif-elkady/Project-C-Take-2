
using UnityEngine;

[CreateAssetMenu(fileName = "New Fireball", menuName = "Spells/Fireball")]
public class FlyingProjectileScriptable : ScriptableObject
{
    public float speed;
    public float baseDamage;
    public float cooldown;
    public DamageTypesSystem.DamageTypes damageType;
    public GameObject onDestroyPrefab;
}

