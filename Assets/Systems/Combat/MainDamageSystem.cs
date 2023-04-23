using System;
using System.Collections;
using UnityEngine;

public class MainDamageSystem : DamageSystem
{
    public override event Action<float> OnHealthDown;
    public override event Action<float> OnHealthUp;
    public override event EventHandler OnDead;
    public event Action OnEnemyKilled;
    public override float MaxHealth { get => _maxHealth; set => _maxHealth = value; }

    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    private bool _isDead;
    private bool _godMode;

    private void Start()
    {
        _currentHealth = _maxHealth;

        OnHealthUp += OnHealHandler;
        OnDead += OnDeadHandler;
        OnEnemyKilled += OnEnemyKilledHandler;
    }

    #region Event Emitters
    public override void TakeDamage(DamageInfo info) => OnDamageTakenHandler(info);
    public override void Heal(float amount) => OnHealthUp?.Invoke(amount);
    public override void Die() => OnDead?.Invoke(this, EventArgs.Empty);
    public void InvokeEnemyKilled() => OnEnemyKilled?.Invoke();
    #endregion

    #region Event Handlers
    private void OnEnemyKilledHandler()
    {
        // TODO: play on kill effects
        print("Enemy killed");
    }

    private void OnDamageTakenHandler(DamageInfo info)
    {
        if (!_godMode)
        {
            var damageTaken = DamageTypesSystem.CalculateDamage(info, DamageTypesSystem.DamageTypes.Air);
            _currentHealth -= damageTaken;
            OnHealthDown(damageTaken);
        }

        if (_currentHealth > 0)
        {
            // TODO: play take damage effects
            // bloodSplashAnimator.Play("BloodSplash");
        }
        else
            OnDead?.Invoke(this, EventArgs.Empty);

    }

    private void OnHealHandler(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    private void OnDeadHandler(object sender, EventArgs e)
    {
        _currentHealth = 0;
        _isDead = true;

        //AudioManager.Instance.Play("PlayerDeath");

        StartCoroutine(DeathCo());
    }
    #endregion
    protected virtual IEnumerator DeathCo()
    {
        //GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(4f);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public override bool GetCurrentState() => _isDead;
    public override float GetHealth() => _currentHealth;




}

