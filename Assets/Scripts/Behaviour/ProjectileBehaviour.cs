using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ProjectileBehaviour : MonoBehaviour {
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleDestroyVFX;

    private WeaponInfo _weaponInfo;
    private Vector3 _startPosition;

    private void Start() {
        _startPosition = transform.position;
    }

    private void Update() {
        MoveProjectile();
        DetectProjectileDistance();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        var indestructible = other.gameObject.GetComponent<IndestructibleBehaviour>();

        if (other.isTrigger) return;
        if (!enemyHealth && !indestructible) return;
        
        Instantiate(particleDestroyVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void UpdateWeaponInfo(WeaponInfo weaponInfo) {
        _weaponInfo = weaponInfo;
    }

    private void DetectProjectileDistance() {
        if (Vector3.Distance(transform.position, _startPosition) > _weaponInfo.weaponRange) {
            Instantiate(particleDestroyVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    
    private void MoveProjectile() {
        transform.Translate(Vector3.right * (moveSpeed * Time.deltaTime));
    }
}
