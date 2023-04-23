
using System;
using UnityEngine;


public class PlayerMain : MonoBehaviour
{
    public PlayerState State{ private get; set; }

    [SerializeField] private BaseController2D _playerController;
    [SerializeField] private AyraAbilityManager _abilityManager;
    private bool jumpRequest = false;
    private float _inputX;
    private void Update()
    {

        if (State != PlayerState.CastingSpell)
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

    public enum PlayerState
    {
        Normal,
        CastingSpell
    }

}

