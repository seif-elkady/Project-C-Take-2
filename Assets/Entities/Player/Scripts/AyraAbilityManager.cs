
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AyraAbilityManager : MonoBehaviour
{
    Dictionary<KeyCode, ISpell> abilities = new Dictionary<KeyCode, ISpell>();
    void Start()
    {
        var fireball = new Fireball();
        fireball.Cast();
        abilities.Add(KeyCode.E, fireball);
    }


    void Update()
    {


        if (Input.GetKeyDown(KeyCode.E))
        {
            if(abilities.TryGetValue(KeyCode.E, out ISpell ability))
            {
                ability.Cast();
            }
        }
    }
}

