
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUi : MonoBehaviour
{
    [SerializeField] public DamageSystem _damageSystem;
    private Slider _healthSlider;
    protected virtual void Start()
    {
        SetupInitialValues();
        SubscribeToDamageEvents();
    }

    private void OnHealthDamage(float amount)
    {
        DecreaseHealth(amount);
    }

    private void OnHeal(float amount)
    {
        AddHealth(amount);
    }

    private void AddHealth(float amount) { _healthSlider.value += amount; }
    private void DecreaseHealth(float amount) 
    { 
        _healthSlider.value -= amount;
        var floatingText = Instantiate(AssetManager.instance.damageTextPrefab, transform.position, Quaternion.identity, UiManager.instance.mainCanvas.transform);
        floatingText.GetComponentInChildren<TMP_Text>().text = ((int)amount).ToString();
    }
    private void SetMaxHealth(float amount) { _healthSlider.maxValue = amount; }

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

