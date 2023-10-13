using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour {
    [SerializeField] private float parallaxOffset = -0.15f;

    private Camera _camera;
    private Vector2 _startingPosition;
    private Vector2 _travel => (Vector2)_camera.transform.position - _startingPosition;

    private void Awake() {
        _camera = Camera.main;
    }

    private void Start() {
        _startingPosition = transform.position;
    }

    private void FixedUpdate() {
        transform.position = _startingPosition + _travel * parallaxOffset;
    }
}
