using UnityEngine;

public class DamageTypesSystem
{
    // Multiplier Matrix, first index for damageType and second is for reciever
    private static float[,] _damageMultipliers = new float[,]{
        { 1f, 1.5f, 0.5f },
        { 0.5f, 1 , 1.5f },
        { 1.5f, 0.5f, 1f },
    };
    
    public static float CalculateDamage(DamageInfo info, DamageTypes recieverType)
    {
        if (info.damageType == DamageTypes.Physical)
            return info.amount;
        float multiplier = _damageMultipliers[(int) info.damageType, (int) recieverType];
        return info.amount * multiplier;
    }
    public enum DamageTypes
    {
        Fire,
        Ice,
        Air, 
        Physical
    }
}


