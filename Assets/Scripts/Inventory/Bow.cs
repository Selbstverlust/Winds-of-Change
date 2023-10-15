using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon {
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    private Animator _animator;
    
    private static readonly int Fire = Animator.StringToHash("Fire");

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }

    public void Attack() {
        _animator.SetTrigger(Fire);
        var newProjectile = Instantiate(projectilePrefab, projectileSpawnPoint.position,
            ActiveWeapon.instance.transform.rotation);
        newProjectile.GetComponent<ProjectileBehaviour>().UpdateWeaponInfo(weaponInfo);
    }
}
