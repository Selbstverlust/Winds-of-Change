using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

public class Sword : MonoBehaviour, IWeapon {
    [SerializeField] private GameObject slashAnimationObject;

    private Transform _weaponCollider;
    private Transform _slashAnimationSpawnPoint;
    private GameObject _slashAnimation;
    private Animator _animator;

    [SerializeField] private WeaponInfo weaponInfo;
    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Start() {
        _weaponCollider = PlayerController.instance.GetWeaponCollider();
        _slashAnimationSpawnPoint = GameObject.Find("Slash Animation Spawn Point").transform;
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    public WeaponInfo GetWeaponInfo() {
        return weaponInfo;
    }

    public void Attack() {
        _animator.SetTrigger("Attack");
        _weaponCollider.gameObject.SetActive(true);
        
        _slashAnimation = Instantiate(slashAnimationObject, _slashAnimationSpawnPoint.position, Quaternion.identity);
        _slashAnimation.transform.parent = transform.parent;
    }
    
    private void SwingFinishedAnimationEvent() {
        _weaponCollider.gameObject.SetActive(false);
    }

    private void SwingUpFlipAnimationEvent() {
        _slashAnimation.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.instance.facingLeft) {
            _slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    private void SwingDownFlipAnimationEvent() {
        _slashAnimation.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        if (PlayerController.instance.facingLeft) {
            _slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset() {
        var mousePos = Input.mousePosition;
        var playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.instance.transform.position);

        var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        
        ActiveWeapon.instance.transform.rotation = mousePos.x < playerScreenPoint.x ? Quaternion.Euler(0, -180, angle) : Quaternion.Euler(0, 0, angle);
        _weaponCollider.transform.rotation = mousePos.x < playerScreenPoint.x ? Quaternion.Euler(0, -180, 0) : Quaternion.Euler(0, 0, 0);
    }
}
