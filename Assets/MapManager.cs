using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public Scene CurrentScene;
    public void OpenLevel(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
    }
    public void CloseLevel()
    {
        if (CurrentScene.isLoaded)
            SceneManager.UnloadSceneAsync(CurrentScene);
    }
}
