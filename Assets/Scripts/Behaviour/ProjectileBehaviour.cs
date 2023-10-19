using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class ProjectileBehaviour : MonoBehaviour {
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleDestroyVFX;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;
    private Vector3 _startPosition;

    private void Start() {
        _startPosition = transform.position;
    }

    private void Update() {
        MoveProjectile();
        DetectProjectileDistance();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var enemy = other.gameObject.GetComponent<EnemyHealth>();
        var player = other.gameObject.GetComponent<PlayerHealth>();
        
        var indestructible = other.gameObject.GetComponent<IndestructibleBehaviour>();

        if (other.isTrigger) return;
        if (!enemy && !indestructible && !player) return;
        if ((player && isEnemyProjectile) || (enemy && !isEnemyProjectile)) {
            player?.TakeDamage(1, transform);
            Instantiate(particleDestroyVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        } else if (!other.isTrigger && indestructible) {
            Instantiate(particleDestroyVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void UpdateProjectileRange(float range) {
        projectileRange = range;
    }
    public void UpdateMoveSpeed(float speed) {
        moveSpeed = speed;
    }

    private void DetectProjectileDistance() {
        if (Vector3.Distance(transform.position, _startPosition) > projectileRange) {
            Instantiate(particleDestroyVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    private void MoveProjectile() {
        transform.Translate(Vector3.right * (moveSpeed * Time.deltaTime));
    }
}
