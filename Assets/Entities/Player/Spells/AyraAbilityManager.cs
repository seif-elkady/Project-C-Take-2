
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AyraAbilityManager : MonoBehaviour
{
    [SerializeField] private Transform _spawnLocation;
    [SerializeField] private BaseController2D _controller;
    [SerializeField] private PlayerMain _playerMain;
    [SerializeField] private LayerMask _abilityTargetMask;
    private ISpell _spell;
    private event Action<float, float> abilityCasted = delegate { };
    readonly Dictionary<KeyCode, ISpell> abilities = new();
    readonly List<KeyCode> spellBindings = new();
    void Start()
    {
        spellBindings.Add(KeyCode.E);
        _spell = GetComponent<Fireball>();
        abilities.Add(spellBindings[0], _spell);
        abilityCasted += HandleAbilityCast;
    }

   

    public void HandleSpellCasting()
    {
        foreach (KeyCode spellKey in spellBindings)
        {
            if (Input.GetKeyDown(spellKey))
                if (abilities.TryGetValue(spellKey, out ISpell ability))
                {
                    if(ability.IsReady())
                    {
                        ability.Cast(_spawnLocation, _abilityTargetMask);
                        var dir = _spawnLocation.position.x > transform.position.x ? 1 : -1;
                        abilityCasted?.Invoke(dir, 0);
                    }
                }
        }
    }
    private void HandleAbilityCast(float dir, float animationTimer)
    {
        _controller.SpriteFlippingHandler(dir);
        StartCoroutine(AbilityCastCo(animationTimer));
    }

    private IEnumerator AbilityCastCo(float timer)
    {
        _playerMain.State = PlayerMain.PlayerState.CastingSpell;
        yield return new WaitForSeconds(timer);
        _playerMain.State = PlayerMain.PlayerState.Normal;
        yield break;

    }


}

