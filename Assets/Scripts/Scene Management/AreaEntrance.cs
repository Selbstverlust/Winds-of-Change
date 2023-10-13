using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour {
    [SerializeField] private string sceneTransitionName;

    private void Start() {
        if (sceneTransitionName == SceneManagement.instance.sceneTransitionName) {
            PlayerController.instance.transform.position = transform.position;
            CameraController.instance.SetPlayerCameraFollow();
            
            UIFade.instance.FadeToClear();
        }
    }
}
