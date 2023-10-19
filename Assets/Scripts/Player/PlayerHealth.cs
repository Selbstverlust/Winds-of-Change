using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackValue = 10f;
    [SerializeField] private float recoveryTime = 1f;

    private int _currentHealth;
    private bool _canTakeDamage = true;
    
    private KnockbackBehaviour _knockback;
    private FlashBehaviour _flash;

    private void Awake() {
        _knockback = GetComponent<KnockbackBehaviour>();
        _flash = GetComponent<FlashBehaviour>();
    }

    private void Start() {
        _currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other) {
        var enemy = other.gameObject.GetComponent<EnemyAI>();

        if (!enemy) return;
        
        TakeDamage(1, other.gameObject.transform);
    }

    public void TakeDamage(int damageAmount, Transform hitTransform) {
        if (!_canTakeDamage) return;
        
        _knockback.GetKnockedBack(hitTransform, knockbackValue);
        _flash.FlashSprite();
        StartCoroutine(RecoveryRoutine());
        _canTakeDamage = false;
        _currentHealth -= damageAmount;
    }

    private IEnumerator RecoveryRoutine() {
        yield return new WaitForSeconds(recoveryTime);
        _canTakeDamage = true;
    }
}
