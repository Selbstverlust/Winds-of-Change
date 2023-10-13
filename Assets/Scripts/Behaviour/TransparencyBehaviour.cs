using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparencyBehaviour : MonoBehaviour {
    [Range(0, 1)] [SerializeField] private float transparencyValue = 0.8f;
    [SerializeField] private float fadeTime = 0.4f;

    private SpriteRenderer _spriteRenderer;
    private Tilemap _tilemap;
    

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.gameObject.CompareTag("Player")) return;
        
        if (_spriteRenderer) {
            StartCoroutine(FadeRoutine(_spriteRenderer, _spriteRenderer.color.a, transparencyValue, fadeTime));
        } else if (_tilemap) {
            StartCoroutine(FadeRoutine(_tilemap, _tilemap.color.a, transparencyValue, fadeTime));
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.gameObject.CompareTag("Player")) return;
        
        if (_spriteRenderer) {
            StartCoroutine(FadeRoutine(_spriteRenderer, transparencyValue, 1f, fadeTime));
        } else if (_tilemap) {
            StartCoroutine(FadeRoutine(_tilemap, transparencyValue, 1f, fadeTime));
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float startValue, float targetValue, float fadingTime) {
        var elapsedTime = 0f;
        while (elapsedTime < fadingTime) {
            elapsedTime += Time.deltaTime;
            var newAlpha = Mathf.Lerp(startValue, targetValue, elapsedTime / fadingTime);
            var color = spriteRenderer.color;
            color = new Color(color.r, color.g, color.b, newAlpha);
            spriteRenderer.color = color;
            yield return null;
        }
    }
    private IEnumerator FadeRoutine(Tilemap tilemap, float startValue, float targetValue, float fadingTime) {
        var elapsedTime = 0f;
        while (elapsedTime < fadingTime) {
            elapsedTime += Time.deltaTime;
            var newAlpha = Mathf.Lerp(startValue, targetValue, elapsedTime / fadingTime);
            var color = tilemap.color;
            color = new Color(color.r, color.g, color.b, newAlpha);
            tilemap.color = color;
            yield return null;
        }
    }
    
}
