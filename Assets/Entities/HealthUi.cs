
using UnityEngine;
using UnityEngine.UI;

public class HealthUi : MonoBehaviour
{
    [SerializeField] public DamageSystem _damageSystem;
    private Slider _healthSlider;
    private void Start()
    {
        SetupInitialValues();
        SubscribeToDamageEvents();
    }

    private void OnHealthDamage(DamageInfo info)
    {
        DecreaseHealth(info.amount);
    }

    private void OnHeal(int amount)
    {
        AddHealth(amount);
    }

    private void AddHealth(int amount) { _healthSlider.value += amount; }
    private void DecreaseHealth(int amount) { _healthSlider.value -= amount; }
    private void SetMaxHealth(int amount) { _healthSlider.maxValue = amount; }

    #region Initial Setup
    private void SetupInitialValues()
    {
        _healthSlider = GetComponent<Slider>();
        SetMaxHealth(_damageSystem.MaxHealth);
        AddHealth((int)_healthSlider.maxValue);
    }
    private void SubscribeToDamageEvents()
    {
        _damageSystem.OnHealthUp += OnHeal;
        _damageSystem.OnHealthDown += OnHealthDamage;
    }

    #endregion

    public void SetDamageSystem(DamageSystem system)
    {
        _damageSystem = system;
    }

}

