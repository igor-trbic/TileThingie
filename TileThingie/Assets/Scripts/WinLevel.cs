using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevel : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float levelSlowmo = 0.2f;

    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel() {
        Time.timeScale = levelSlowmo;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1f;
        var currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIdx + 1);
    }
}
