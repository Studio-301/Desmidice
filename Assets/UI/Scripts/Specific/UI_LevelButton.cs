using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_LevelButton : MonoBehaviour
{
    [SerializeField] TMP_Text label;

    Action onClick;
    public void Set(string name, Action openLevel)
    {
        label.text = name;
        onClick = openLevel;
    }

    public void SelectLevel() => onClick?.Invoke();
}
