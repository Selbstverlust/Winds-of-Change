using Unity.Mathematics;
using UnityEngine;

public class DestructibleBehaviour : MonoBehaviour {
    [SerializeField] private GameObject destructionVFX;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.gameObject.GetComponent<DamageSource>()) return;
        
        Instantiate(destructionVFX, transform.position, quaternion.identity);
        Destroy(gameObject);
    }
}
