using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private float roamChangeDirectionTime = 2f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private bool stopMovingWhileAttacking = false;
    private enum State {
        Roaming,
        Attacking
    }

    private State _state;
    private EnemyPathfinding _enemyPathfinding;
    private Vector2 _roamPosition;
    
    private float _timeRoaming = 0f;
    private bool _canAttack = true;
    
    private void Awake() {
        _state = State.Roaming;
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start() {
        _roamPosition = GetRoamingPosition();
    }

    private void Update() {
        MovementStateControl();
    }

    private void MovementStateControl() {
        switch (_state) {
            case State.Roaming:
                Roaming();
            break;
            case State.Attacking:
                Attacking();
            break;
        }
    }

    private void Roaming() {
        _timeRoaming += Time.deltaTime;
        _enemyPathfinding.MoveTo(_roamPosition);

        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) < attackRange) {
            _state = State.Attacking;
        }
        if (_timeRoaming > roamChangeDirectionTime) {
            _roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking() {
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) > attackRange) {
            _state = State.Roaming;
        }
        
        if (attackRange == 0 || !_canAttack) return;

        if (stopMovingWhileAttacking) {
            _enemyPathfinding.StopMoving();
        } else {
            _enemyPathfinding.MoveTo(_roamPosition);
        }
        _canAttack = false;
        StartCoroutine(AttackCooldownRoutine());
        (enemyType as IEnemy)?.Attack();
    }

    private IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }

    private Vector2 GetRoamingPosition() {
        _timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
