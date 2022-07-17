using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public UnityEvent OnMapSelected;
    [SerializeField] UIManager uiManager;

    [System.Serializable]
    public class LevelInfo
    {
        public string Name;
        public string ID;
    }

    public LevelInfo[] Levels;
    Scene currentScene;
    public void OpenLevel(LevelInfo info)
    {
        CloseLevel(() =>
        {
            Debug.Log($"opening: {info.Name} => {info.ID}");

            var operation = SceneManager.LoadSceneAsync(info.ID, LoadSceneMode.Additive);
            currentScene = SceneManager.GetSceneByName(info.ID);

            operation.completed += (a) => OnMapSelected?.Invoke();
        });
    }
    void CloseLevel(Action onUnload = null)
    {
        if (currentScene.isLoaded)
        {
            var operation = SceneManager.UnloadSceneAsync(currentScene);
            operation.completed += (a) => onUnload?.Invoke();
        }
        else
            onUnload?.Invoke();
    }

    public void Begin()
    {
        var level = Levels.First();
        OpenLevel(level);
    }

    public void Next()
    {
        int nextLevelIndex = int.MaxValue;
        for (int i = 0; i < Levels.Length; i++)
        {
            var level = Levels[i];

            if (level.ID == currentScene.name)
                nextLevelIndex = i + 1;
        }

        if (nextLevelIndex > Levels.Length)
        {
            CloseLevel();
            uiManager.ShowCredits();
        }
        else
            OpenLevel(Levels[nextLevelIndex]);
    }
}
