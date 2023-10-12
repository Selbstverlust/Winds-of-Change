using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private PlayerControls _playerControls;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;
    
    private Vector2 _movement;
    [SerializeField] private float moveSpeed = 1f;
    
    // Cached animation strings.
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    

    private void Awake() {
        // Assigns variables on Awake.
        _camera = Camera.main;
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        _playerControls.Enable();
    }

    private void Update() {
        PlayerInput();
    }

    private void FixedUpdate() {
        Move();
        AdjustPlayerFacingDirection();
    }

    private void PlayerInput() {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        
        _animator.SetFloat(MoveX, _movement.x);
        _animator.SetFloat(MoveY, _movement.y);
    }

    private void Move() {
        _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection() {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = _camera.WorldToScreenPoint(transform.position);
        _spriteRenderer.flipX = mousePos.x < playerScreenPoint.x;
    }
}
