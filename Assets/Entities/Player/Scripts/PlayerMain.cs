
using System;
using UnityEngine;


public class PlayerMain : MonoBehaviour
{
    [SerializeField] private BaseController2D _playerController;
    private bool jumpRequest = false;
    private float _inputX;
    void Start()
    {
        
    }

    private void Update()
    {
        HandleHorizontalMovement();
        HandleJumping();
        
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

    }

    private void HandleHorizontalMovement()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequest = true;
        }
    }

}

