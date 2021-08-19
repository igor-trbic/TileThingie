using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{

    private int startingSceneIdx;
    private void Awake() {
        int numScenePersists = FindObjectsOfType<GameSession>().Length;
        if (numScenePersists > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        startingSceneIdx = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int currSceneIdx = SceneManager.GetActiveScene().buildIndex;
        if (currSceneIdx != startingSceneIdx) {
            Destroy(gameObject);
        }
    }
}
