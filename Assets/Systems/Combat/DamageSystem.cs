using System;
using UnityEngine;

public abstract class DamageSystem : MonoBehaviour
{
    public abstract event Action<float> OnHealthDown;
    public abstract event Action<float> OnHealthUp;
    public abstract event EventHandler OnDead;
    public abstract float MaxHealth { get; set; }

    public abstract void TakeDamage(DamageInfo info);
    public abstract void Heal(float amount);
    public abstract void Die();
    public abstract bool GetCurrentState();
    public abstract float GetHealth();
}

