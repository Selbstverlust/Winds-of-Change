using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour {
    
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private KnockbackBehaviour _knockbackBehaviour;
        
    private Vector2 _movement;
    [SerializeField] private float moveSpeed = 1f;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _knockbackBehaviour = GetComponent<KnockbackBehaviour>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        Move();
        AdjustSpriteDirection();
    }

    private void Move() {
        if (_knockbackBehaviour.gettingKnockedBack) { return; }
        
        _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition) {
        _movement = targetPosition;
    }

    public void StopMoving() {
        _movement = Vector3.zero;
    }

    private void AdjustSpriteDirection() {
        _spriteRenderer.flipX = _movement.x < Mathf.Epsilon;
    }
}
