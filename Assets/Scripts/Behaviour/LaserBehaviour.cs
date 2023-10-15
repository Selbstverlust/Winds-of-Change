using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {
    [SerializeField] private float laserGrowTime = 2f;
    private float _laserRange;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider;

    private bool _isGrowing = true;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start() {
        LaserFaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<IndestructibleBehaviour>() && !other.isTrigger) _isGrowing = false;
    }

    public void UpdateLaserRange(float laserRange) {
        _laserRange = laserRange;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private IEnumerator IncreaseLaserLengthRoutine() {
        var timePassed = 0f;
        
        while (_spriteRenderer.size.x < _laserRange && _isGrowing) {
            timePassed += Time.deltaTime;
            var linearT = timePassed / laserGrowTime;
            
            _spriteRenderer.size = new Vector2(Mathf.Lerp(1f, _laserRange, linearT), 1f);
            
            _capsuleCollider.size = new Vector2(Mathf.Lerp(1f, _laserRange, linearT), _capsuleCollider.size.y);
            _capsuleCollider.offset = new Vector2((Mathf.Lerp(1f, _laserRange, linearT) / 2), _capsuleCollider.offset.y);

            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFadeBehaviour>().SlowFadeRoutine());
    }

    private void LaserFaceMouse() {
        var mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }
}
