using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBehaviour : MonoBehaviour {
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float flashTime = .2f;

    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;
    
    public bool flashingSprite { get; private set; }

    public float GetFlashTime() { return flashTime; }

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public void FlashSprite() {
        flashingSprite = true;
        StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine() {
        _spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(flashTime);
        _spriteRenderer.material = _defaultMaterial;
        flashingSprite = false;
    }
}
