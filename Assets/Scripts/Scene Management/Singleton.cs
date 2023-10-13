using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {
    public static T instance { get; private set; }
    protected virtual void Awake() {
        if (instance != null && this.gameObject != null) {
            Destroy(this.gameObject);
        } else {
            instance = (T)this;
        }

        if (!gameObject.transform.parent) {
            DontDestroyOnLoad(gameObject);
        }
    }
}
