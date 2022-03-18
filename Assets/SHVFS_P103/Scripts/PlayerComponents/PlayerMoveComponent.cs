using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveComponent : MonoBehaviour
{
    public float MoveSpeed = 2f;
    
    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector3 _playerMoveInput;
    
    private static readonly int RightVelocity = Animator.StringToHash("RightVelocity");
    private static readonly int ForwardVelocity = Animator.StringToHash("ForwardVelocity");

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        var rightInput = Input.GetAxis("Horizontal");
        var forwardInput = Input.GetAxis("Vertical");
        var playerTransform = transform;
        
        _playerMoveInput = rightInput * playerTransform.right + forwardInput * playerTransform.forward;
        _playerMoveInput = _playerMoveInput.magnitude > 1 ? _playerMoveInput.normalized : _playerMoveInput;

        if (!_animator) return;
        _animator.SetFloat(RightVelocity, rightInput);
        _animator.SetFloat(ForwardVelocity, forwardInput);
    }

    private void FixedUpdate()
    {
        if(!_rigidbody) return;
        
        _rigidbody.MovePosition(transform.position + _playerMoveInput * MoveSpeed * Time.fixedDeltaTime);
    }
}
