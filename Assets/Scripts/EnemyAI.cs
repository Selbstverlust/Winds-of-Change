using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour {
    private enum State {
        Roaming
    }

    private State _state;
    private EnemyPathfinding _enemyPathfinding;
    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    
    private Vector2 _movement;
    [SerializeField] private float moveSpeed = 1f;

    private void Awake() {
        _state = State.Roaming;
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    private void FixedUpdate() {
        Move();
        AdjustSpriteDirection();
    }

    private IEnumerator RoamingRoutine() {
        while (_state == State.Roaming) {
            Vector2 roamPosition = GetRoamingPosition();
            _movement = roamPosition;
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
    
    private void Move() {
        _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustSpriteDirection() {
        _spriteRenderer.flipX = _movement.x < Mathf.Epsilon;
    }
}
