using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour {
    public static PlayerController Instance;
    private PlayerControls _playerControls;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;
    private float _defaultMoveSpeed;
    
    [SerializeField] private TrailRenderer trailRenderer;

    private bool _isDashing;
    private bool _facingLeft;
    public bool facingLeft { get => _facingLeft; }
    
    private Vector2 _movement;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 0.5f;
    
    // Cached animation strings.
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    

    private void Awake() {
        // Assigns variables on Awake.
        Instance = this;
        _camera = Camera.main;
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        _playerControls.Movement.Dash.performed += _ => Dash();
        _defaultMoveSpeed = moveSpeed;
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

    private void Dash() {
        if (_isDashing) return;
        _isDashing = true;
        moveSpeed *= dashSpeed;
        trailRenderer.emitting = true;
        StartCoroutine(DashRoutine());
    }

    private IEnumerator DashRoutine() {
        yield return new WaitForSeconds(dashTime);
        moveSpeed = _defaultMoveSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
        _isDashing = false;

    }

    private void AdjustPlayerFacingDirection() {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = _camera.WorldToScreenPoint(transform.position);
        _spriteRenderer.flipX = mousePos.x < playerScreenPoint.x;
        _facingLeft = mousePos.x < playerScreenPoint.x;
        
    }
}
