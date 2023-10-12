using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class Sword : MonoBehaviour {
    [SerializeField] private GameObject slashAnimationObject;
    [SerializeField] private Transform slashAnimationSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    
    
    private GameObject _slashAnimation;
    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private Camera _camera;
    private void Awake() {
        _playerControls = new PlayerControls();
        _animator = GetComponent<Animator>();
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _camera = Camera.main;
    }
    
    private void OnEnable() {
        _playerControls.Enable();
    }

    private void Start() {
        _playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    private void Attack() {
        _animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        
        _slashAnimation = Instantiate(slashAnimationObject, slashAnimationSpawnPoint.position, quaternion.identity);
        _slashAnimation.transform.parent = this.transform.parent;
    }

    private void SwingUpFlipAnimationEvent() {
        _slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (_playerController.facingLeft) {
            _slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    private void SwingDownFlipAnimationEvent() {
        if (_playerController.facingLeft) {
            _slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void SwingFinishedAnimationEvent() {
        weaponCollider.gameObject.SetActive(false);
    }

    private void MouseFollowWithOffset() {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = _camera.WorldToScreenPoint(_playerController.transform.position);

        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        _activeWeapon.transform.rotation = mousePos.x < playerScreenPoint.x ? Quaternion.Euler(0, -180, angle) : Quaternion.Euler(0, 0, angle);
        weaponCollider.transform.rotation = mousePos.x < playerScreenPoint.x ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);
    }
}
