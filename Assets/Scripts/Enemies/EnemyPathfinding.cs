using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour {
    
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private EnemyAI _enemyAI;
        
    private Vector2 _movement;
    [SerializeField] private float moveSpeed = 1f;
    
    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyAI = GetComponent<EnemyAI>();
    }

    private void FixedUpdate() {
        Move();
        AdjustSpriteDirection();
    }

    private void Move() {
        _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition) {
        _movement = targetPosition;
    }

    private void AdjustSpriteDirection() {
        _spriteRenderer.flipX = _movement.x < Mathf.Epsilon;
    }
}
