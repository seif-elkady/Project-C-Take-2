using System.Collections;
using UnityEngine;

public class PlayerDamageSystem : MainDamageSystem
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(new DamageInfo(50f, DamageTypesSystem.DamageTypes.Fire));
        }
    }
    protected override IEnumerator DeathCo()
    {
        yield return new WaitForSeconds(4f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}

