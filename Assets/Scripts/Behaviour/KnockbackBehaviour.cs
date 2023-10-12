using System.Collections;
using UnityEngine;

public class KnockbackBehaviour : MonoBehaviour {
    public bool gettingKnockedBack { get; private set; }
    
    private Rigidbody2D _rb;

    [SerializeField] private float knockbackTime = 0.2f;

    private void Awake() {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void GetKnockedBack(Transform damageSource, float knockbackValue) {
        gettingKnockedBack = true;
        var impulse = (transform.position - damageSource.position).normalized * knockbackValue * _rb.mass;
        _rb.AddForce(impulse, ForceMode2D.Impulse);
        StartCoroutine(KnockbackRoutine());
    }

    private IEnumerator KnockbackRoutine() {
        yield return new WaitForSeconds(knockbackTime);

        _rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
