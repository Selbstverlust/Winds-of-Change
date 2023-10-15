using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFadeBehaviour : MonoBehaviour {
    [SerializeField] private float fadeTime = .4f;

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator SlowFadeRoutine() {
        var elapsedTime = 0f;
        var startValue = _spriteRenderer.color.a;
            
        while (elapsedTime < fadeTime) {
            elapsedTime += Time.deltaTime;
            var newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeTime);
            var color = _spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, newAlpha);
            _spriteRenderer.color = color;
            yield return null;
        }
        
        Destroy(gameObject);
    }
}
