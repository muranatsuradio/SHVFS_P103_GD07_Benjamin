using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotateComponent : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;
    private Camera _mainCamera;

    private Vector3 _direction;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!_mainCamera) return;

        if (!PlayerInputSystem.Instance.CanPlayerRotate) return;

        var mouseScreenPosition = Input.mousePosition;
        var playerScreenPosition = _mainCamera.WorldToScreenPoint(transform.position);
        
        _direction = mouseScreenPosition - playerScreenPosition;
    }

    private void FixedUpdate()
    {
        if(!_rigidbody) return;

        var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        
        _rigidbody.MoveRotation(Quaternion.AngleAxis((90f - angle), transform.up));
    }
}