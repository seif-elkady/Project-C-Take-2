
using System;
using UnityEngine;


public class PlayerMain : MonoBehaviour
{
    public PlayerState State{ private get; set; }

    [SerializeField] private BaseController2D _playerController;
    [SerializeField] private AyraAbilityManager _abilityManager;
    [SerializeField] private DamageSystem _damageSystem;
    private bool jumpRequest = false;
    private float _inputX;

    private void Start()
    {
        _damageSystem.OnDead += HandleDeath; 
    }


    private void Update()
    {
        if (State != PlayerState.CastingSpell && State != PlayerState.Dead)
        {
            HandleHorizontalMovement();
            HandleJumping();

            HandleAbilities();
        }
    }
    private void HandleAbilities()
    {
        _abilityManager.HandleSpellCasting();
    }

    void FixedUpdate()
    {
        _playerController.SlopeCheck();
        _playerController.Move(_inputX);
        if (jumpRequest)
        {
            _playerController.Jump();
            jumpRequest = false;
        }
        Reset();
    }

    private void HandleHorizontalMovement()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
    }

    private void HandleJumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpRequest = true;
        }
    }
    private void Reset()
    {
        _inputX = 0;
    }
    private void HandleDeath(object sender, EventArgs e)
    {
        State = PlayerState.Dead;
    }

    public enum PlayerState
    {
        Normal,
        CastingSpell,
        Dead
    }

}

