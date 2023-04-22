using System;
using UnityEngine;

public abstract class DamageSystem : MonoBehaviour
{
    public abstract event Action<DamageInfo> OnHealthDown;
    public abstract event Action<int> OnHealthUp;
    public abstract event EventHandler OnDead;
    public abstract int MaxHealth { get; set; }

    public abstract void TakeDamage(DamageInfo info);
    public abstract void Heal(int amount);
    public abstract void Die();
    public abstract bool GetCurrentState();
    public abstract int GetHealth();
}

