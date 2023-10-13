using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private float roamFloatTimer = 2f;
    private enum State {
        Roaming
    }

    private State _state;
    private EnemyPathfinding _enemyPathfinding;
    
    private void Awake() {
        _state = State.Roaming;
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
    }

    private void Start() {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine() {
        while (_state == State.Roaming) {
            var roamPosition = GetRoamingPosition();
            _enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamFloatTimer);
        }
    }

    private Vector2 GetRoamingPosition() {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
