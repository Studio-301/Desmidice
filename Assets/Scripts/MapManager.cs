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

    [Serializable]
    public class LevelInfo
    {
        public string Name;
        public string ID;
    }

    public LevelInfo[] Levels;
    public Scene currentScene;
    public LevelInfo currentInfo;

    float levelStart;
    public void OpenLevel(LevelInfo info)
    {
        uiManager.ShowBlocker(() =>
        {
            currentInfo = info;
            CloseLevel(() =>
            {
                levelStart = Time.time;
                Debug.Log($"opening: {info.Name} => {info.ID}");

                var operation = SceneManager.LoadSceneAsync(info.ID, LoadSceneMode.Additive);
                currentScene = SceneManager.GetSceneByName(info.ID);

                FindObjectOfType<DiceManipulator>().enabled = true;

                operation.completed += (a) =>
                {
                    OnMapSelected?.Invoke();
                    uiManager.HideBlocker();
                };
            });
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

    public float GetLevelDuration() => Time.time - levelStart;

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

        if (nextLevelIndex >= Levels.Length)
        {
            //CloseLevel();
            uiManager.ShowCredits();
        }
        else
        {
            uiManager.ShowGame();
            OpenLevel(Levels[nextLevelIndex]);
        }
    }
}
