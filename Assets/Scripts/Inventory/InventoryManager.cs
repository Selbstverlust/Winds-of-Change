using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour {
    private int _activeSlotIndexNum = 0;
    private PlayerControls _playerControls;

    private void Awake() {
        _playerControls = new PlayerControls();
    }

    private void Start() {
        _playerControls.Inventory.Keyboard.performed += context => ToggleActiveSlot((int)context.ReadValue<float>());
        
        ToggleActiveHighlight(0);
    }

    private void OnEnable() {
        _playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue) {
        ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int indexNum) {
        _activeSlotIndexNum = indexNum;

        foreach (Transform inventorySlot in transform) {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        
        transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);
        
        ChangeActiveWeapon();
    }
    private void ChangeActiveWeapon() {
        if (ActiveWeapon.instance.currentActiveWeapon != null) {
            Destroy(ActiveWeapon.instance.currentActiveWeapon.gameObject);
        }

        if (transform.GetChild(_activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo() == null) {
            ActiveWeapon.instance.WeaponNull();
            return;
        }
        var weaponToSpawn = transform.GetChild(_activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;
        var newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.instance.transform.position, Quaternion.identity);
        ActiveWeapon.instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        
        newWeapon.transform.parent = ActiveWeapon.instance.transform;
        ActiveWeapon.instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
