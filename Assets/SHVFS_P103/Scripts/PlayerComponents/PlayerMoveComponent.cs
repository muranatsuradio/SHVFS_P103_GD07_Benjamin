using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMoveComponent : MonoBehaviour
{
    public const float MAX_SPEED = 3f;
    public float MoveSpeed = MAX_SPEED;
    
    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector3 _playerMoveInput;

    private bool _canMove;

    private static readonly int RightVelocity = Animator.StringToHash("RightVelocity");
    private static readonly int ForwardVelocity = Animator.StringToHash("ForwardVelocity");

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!PlayerInputSystem.Instance.CanPlayerMove) return;

        var rightInput = Input.GetAxis("Horizontal");
        var forwardInput = Input.GetAxis("Vertical");

        _playerMoveInput = rightInput * Vector3.right + forwardInput * Vector3.forward;
        _playerMoveInput = _playerMoveInput.magnitude > 1 ? _playerMoveInput.normalized : _playerMoveInput;

        if (!_animator) return;

        var right = transform.right;
        var forward = transform.forward;

        var xInputToRight = Vector3.Project(rightInput * Vector3.right, right);
        var xInputToForward = Vector3.Project(rightInput * Vector3.right, forward);
        var zInputToRight = Vector3.Project(forwardInput * Vector3.forward, right);
        var zInputToForward = Vector3.Project(forwardInput * Vector3.forward, forward);

        var rightVector = xInputToRight + zInputToRight;
        var forwardVector = xInputToForward + zInputToForward;

        var rightVelocity = rightVector.magnitude * Mathf.Cos(Vector3.Angle(rightVector, right));
        var forwardVelocity = forwardVector.magnitude * Mathf.Cos(Vector3.Angle(forwardVector, forward));

        _animator.SetFloat(RightVelocity, rightVelocity);
        _animator.SetFloat(ForwardVelocity, forwardVelocity);
    }

    private void FixedUpdate()
    {
        if (!_rigidbody) return;
        
        _rigidbody.MovePosition(transform.position + _playerMoveInput * MoveSpeed * Time.fixedDeltaTime);

        if (PlayerInputSystem.Instance.CanPlayerMove) return;

        _rigidbody.MovePosition(transform.position);
    }

    public static void SetPlayerCanMove(bool canMove)
    {
        PlayerInputSystem.Instance.CanPlayerMove = canMove;
    }
}