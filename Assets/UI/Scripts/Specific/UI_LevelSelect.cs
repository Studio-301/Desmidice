using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LevelSelect : MonoBehaviour
{
    [SerializeField] MapManager mapManager;
    [SerializeField] UI_LevelButton levelButton;
    [SerializeField] Transform levelButtonsRoot;

    void Awake()
    {
        foreach(var x in mapManager.Levels)
        {
            var btn = Instantiate(levelButton, levelButtonsRoot);
            btn.Set(x.Name, () => mapManager.OpenLevel(x));
        }
    }
}
