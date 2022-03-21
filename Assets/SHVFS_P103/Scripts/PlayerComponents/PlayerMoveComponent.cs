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
        if (!PlayerInputSystem.Instance.CanPlayerInput) return;

        var rightInput = Input.GetAxis("Horizontal");
        var forwardInput = Input.GetAxis("Vertical");
        var playerTransform = transform;

        _playerMoveInput = rightInput * Vector3.right + forwardInput * Vector3.forward;
        _playerMoveInput = _playerMoveInput.magnitude > 1 ? _playerMoveInput.normalized : _playerMoveInput;

        if (!_animator) return;
        
        var rightInputToX = Vector3.Project(rightInput * Vector3.right, transform.right).magnitude;
        var rightInputToZ = Vector3.Project(rightInput * Vector3.right, transform.forward).magnitude;
        var forwardInputToX = Vector3.Project(forwardInput * Vector3.forward, transform.right).magnitude;
        var forwardInputToZ = Vector3.Project(forwardInput * Vector3.forward, transform.forward).magnitude;
        
        _animator.SetFloat(RightVelocity, rightInputToX + forwardInputToX);
        _animator.SetFloat(ForwardVelocity, rightInputToZ + forwardInputToZ);
    }

    private void FixedUpdate()
    {
        if (!_rigidbody) return;

        _rigidbody.MovePosition(transform.position + _playerMoveInput * MoveSpeed * Time.fixedDeltaTime);

        if (PlayerInputSystem.Instance.CanPlayerInput) return;
        _rigidbody.MovePosition(transform.position);
    }
}