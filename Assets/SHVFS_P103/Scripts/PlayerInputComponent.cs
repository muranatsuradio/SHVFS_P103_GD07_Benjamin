using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

#region Notes
//Rename   ->   ctrl + R
#endregion

public class PlayerInputComponent : MonoBehaviour
{
    public float MovementSpeed;
    public float JumpHeight;
    public float RayOffsetX;
    public float RayOffsetY;
    public float LookSpeed;
    public Transform CameraContainer;

    private const float _RAYLENGTH = 1.2f;

    private Rigidbody _rigidbody;
    private Animator _animator;

    private Vector3 _processedMoveInput;
    private float _processedTurnInput;
    private float _processedLookInput;

    private float _rotateSpeed = 60.0f;
    private bool _isJumping;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }
    
    private void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        var rightInput = Input.GetAxis("Horizontal");
        var forwardInput = Input.GetAxis("Vertical");

        var isGrounded = IsGrounded();

        _processedTurnInput = Input.GetAxis("Mouse X");
        _processedLookInput = -Input.GetAxis("Mouse Y");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            _isJumping = true;
        }

        _processedMoveInput = transform.forward * forwardInput + transform.right * rightInput;
        _processedMoveInput = _processedMoveInput.magnitude >= 1 ? _processedMoveInput.normalized : _processedMoveInput;

        CameraContainer.Rotate(new Vector3(_processedLookInput, .0f, .0f) * LookSpeed * Time.deltaTime);

        if (_animator)
        {
            _animator.SetFloat("HorizontalInput", rightInput);
            _animator.SetFloat("VerticalInput", forwardInput);
            _animator.SetBool("IsJumping", !isGrounded);
        }
    }
    
    private void FixedUpdate()
    {
        _rigidbody.MoveRotation(Quaternion.Euler(transform.eulerAngles + (Vector3.up * _processedTurnInput) * Time.fixedDeltaTime * _rotateSpeed));

        if (_isJumping)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(JumpHeight * Vector3.up, ForceMode.Impulse);
            _isJumping = false;
        }
        
        _rigidbody.MovePosition(transform.position + _processedMoveInput * MovementSpeed * Time.fixedDeltaTime);
    }
    
    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector3.down * _RAYLENGTH, Color.red);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit raycastHit, _RAYLENGTH))
        {
            if (raycastHit.collider.gameObject.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }
}
