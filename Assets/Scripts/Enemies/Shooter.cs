using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy {
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;

    private bool _isShooting = false;
    
    public void Attack() {
        if (!_isShooting) {
            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine() {
        _isShooting = true;

        for (var i = 0; i < burstCount; i++) {
            var targetDirection = (Vector2)(PlayerController.instance.transform.position - transform.position);
            var newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            newProjectile.transform.right = targetDirection;

            if (newProjectile.TryGetComponent(out ProjectileBehaviour projectile)) {
                projectile.UpdateMoveSpeed(projectileMoveSpeed);
            }

            yield return new WaitForSeconds(timeBetweenBursts);
        }

        yield return new WaitForSeconds(restTime);
        _isShooting = false;
    }
}
