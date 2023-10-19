using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class USP : MonoBehaviour, IWeapon {
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private SpriteRenderer _sprite;

    private Vector3 _projectileSpawnPointStartPosition;

    private void Awake() {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        _projectileSpawnPointStartPosition = projectileSpawnPoint.localPosition;
    }

    private void Update() {
        FlipAxis();
    }

    private void FlipAxis() {
        _sprite.flipY = PlayerController.instance.facingLeft;
        if (PlayerController.instance.facingLeft) {
            projectileSpawnPoint.localPosition = new Vector3(_projectileSpawnPointStartPosition.x, -_projectileSpawnPointStartPosition.y);
        } else {
            projectileSpawnPoint.localPosition = _projectileSpawnPointStartPosition;
        }
    }
    
    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }

    public void Attack() {
        var newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, ActiveWeapon.instance.transform.rotation);
        
        newProjectile.GetComponent<ProjectileBehaviour>().UpdateProjectileRange(weaponInfo.weaponRange);
    }
}
