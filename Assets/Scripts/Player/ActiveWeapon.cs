using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ActiveWeapon : Singleton<ActiveWeapon> {
    public MonoBehaviour currentActiveWeapon { get; private set; }
    
    private PlayerControls _playerControls;
    private float _currentCooldown;
    
    private bool _isAttacking;
    private bool _attackButtonDown;

    protected override void Awake() {
        base.Awake();
        
        _playerControls = new PlayerControls();
    }

    private void OnEnable() {
        _playerControls.Enable();
    }
    
    private void Start() {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
        
        AttackCooldown();
    }

    private void Update() {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon) {
        currentActiveWeapon = newWeapon;
        AttackCooldown();
        _currentCooldown = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull() {
        currentActiveWeapon = null;
    }

    private void AttackCooldown() {
        _isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(AttackCooldownRoutine());
    }

    private IEnumerator AttackCooldownRoutine() {
        yield return new WaitForSeconds(_currentCooldown);
        _isAttacking = false;
    }
    
    private void StartAttacking() {
        _attackButtonDown = true;
    }

    private void StopAttacking() {
        _attackButtonDown = false;
    }

    private void Attack() {
        if (!_attackButtonDown) return;
        if (_isAttacking) return;
        if (!currentActiveWeapon) return;
        
        AttackCooldown();
        (currentActiveWeapon as IWeapon).Attack();
    }
}
