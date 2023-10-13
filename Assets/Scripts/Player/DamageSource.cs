using UnityEngine;

public class DamageSource : MonoBehaviour {
    [SerializeField] private int damageValue = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        
        if (!enemyHealth) return;
        enemyHealth.TakeDamage(damageValue);
    }
}
