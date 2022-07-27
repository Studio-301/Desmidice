using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLogic : MonoBehaviour
{
    void Awake()
    {
        var singleton = FindObjectOfType<Singleton>();
        if (singleton == null)
        {
            var curr = SceneManager.GetActiveScene();
            StartCoroutine(ReloadSceneWithMain(curr));
        }
        else
            Destroy(gameObject);
    }

    IEnumerator ReloadSceneWithMain(Scene originalLevel)
    {
        Debug.Log("LoadMainScene");

        yield return SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive); //logic scene

        FindObjectOfType<MapManager>().OpenLevel(new MapManager.LevelInfo()
        {
            Name = "DEV",
            ID = originalLevel.name
        });

        yield return SceneManager.UnloadSceneAsync(originalLevel);
    }
}
