using UnityEngine;

public class DamageInfo 
{

    public Transform damager;
    public float force;
    public Vector3 damagerDirection;
    public Vector3 collisionPoint;
    public DamageTypesSystem.DamageTypes damageType;
    public float amount;



    public DamageInfo(float amount, DamageTypesSystem.DamageTypes damageType, Transform damager, Vector2 damagerDirection, float force, Vector2 collisionPoint)
    {
        this.amount = amount;
        this.damageType = damageType;
        this.damager = damager;
        this.damagerDirection = damagerDirection;
        this.force = force;
        this.collisionPoint = collisionPoint;
    }

    public DamageInfo(float amount, DamageTypesSystem.DamageTypes damageType)
    {
        this.damageType = damageType;
        this.amount = amount;
        this.damager = null;
        this.damagerDirection = Vector3.zero;
        this.force = 0f;
        this.collisionPoint = Vector3.zero;
    }


}
