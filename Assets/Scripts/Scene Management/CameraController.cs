using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : Singleton<CameraController> {
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    private void Start() {
        SetPlayerCameraFollow();
    }

    public void SetPlayerCameraFollow() {
        _cinemachineVirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _cinemachineVirtualCamera.Follow = PlayerController.instance.transform;
    }
}
