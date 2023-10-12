using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfEvent : MonoBehaviour {
    private void DestroySelf() {
        Destroy(gameObject);
    }
}
