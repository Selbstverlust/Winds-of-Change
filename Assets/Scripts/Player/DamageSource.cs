using UnityEngine;

public class DamageSource : MonoBehaviour {
    [SerializeField] private int damageValue = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.GetComponent<EnemyHealth>()) {
            var enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageValue);
        }
    }
}
