using System;
using UnityEngine;

public class DamageSource : MonoBehaviour {
    private int _damageValue = 1;

    private void Start() {
        var currentActiveWeapon = ActiveWeapon.instance.currentActiveWeapon;
        _damageValue = (currentActiveWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        
        if (!enemyHealth) return;
        enemyHealth.TakeDamage(_damageValue);
    }
}
