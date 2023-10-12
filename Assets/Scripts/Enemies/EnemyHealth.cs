using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;

    private int _currentHealth;
    private KnockbackBehaviour _knockbackBehaviour;
    private FlashBehaviour _flashBehaviour;

    private void Start() {
        _currentHealth = startingHealth;
        _knockbackBehaviour = GetComponent<KnockbackBehaviour>();
        _flashBehaviour = GetComponent<FlashBehaviour>();
    }

    public void TakeDamage(int damage) {
        _currentHealth -= damage;
        _knockbackBehaviour.GetKnockedBack(PlayerController.Instance.transform, 15f);
        _flashBehaviour.FlashSprite();
        StartCoroutine(WaitForFlash());
    }

    private IEnumerator WaitForFlash() {
        yield return new WaitForSeconds(_flashBehaviour.GetFlashTime());
        DetectDeath();
    }
    private void DetectDeath() {
        if (_currentHealth <= 0) {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
