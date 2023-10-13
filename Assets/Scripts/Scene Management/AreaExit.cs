using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour {
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    [SerializeField] private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.gameObject.GetComponent<PlayerController>()) return;
        
        UIFade.instance.FadeToBlack();
        StartCoroutine(LoadSceneRoutine());
    }

    private IEnumerator LoadSceneRoutine() {
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManagement.instance.SetTransitionName(sceneTransitionName);
        SceneManager.LoadScene(sceneToLoad);
    }
}
