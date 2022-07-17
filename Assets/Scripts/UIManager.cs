using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UI_Page[] menuElements;
    [SerializeField] UI_Page mainMenu;
    [SerializeField] UI_Page gameUI;
    [SerializeField] UI_Page credits;
    [SerializeField] UI_Page nextLevel;

    [SerializeField] UI_Page background;

    [SerializeField] MapManager mapManager;
    [SerializeField] TMP_Text levelTimer;

    public void ShowGame()
    {
        HideWholeMenu();
        gameUI.Show();
        background.Hide();
    }
    public void ShowMenu()
    {
        HideWholeMenu();
        gameUI.Hide();
        mainMenu.Show();
        background.Show();
    }
    public void ShowCredits()
    {
        HideWholeMenu();
        gameUI.Hide();
        credits.Show();
        background.Show();
    }

    public void ShowNextLevel()
    {
        var t = mapManager.GetLevelDuration();
        int minutes = (int)(t / 60);
        int seconds = (int)(t % 60);
        levelTimer.text = $"You finished {mapManager.currentInfo.Name} in {minutes:00}:{seconds:00}";

        HideWholeMenu();
        gameUI.Hide();
        nextLevel.Show();
        background.Hide();
    }

    void HideWholeMenu()
    {
        foreach (var x in menuElements)
            x.Hide();
    }
}
