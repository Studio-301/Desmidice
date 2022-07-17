using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UI_Page[] menuElements;
    [SerializeField] UI_Page mainMenu;
    [SerializeField] UI_Page gameUI;
    [SerializeField] UI_Page credits;
    [SerializeField] UI_Page nextLevel;

    public void ShowGame()
    {
        HideWholeMenu();
        gameUI.Show();
    }
    public void ShowMenu()
    {
        HideWholeMenu();
        gameUI.Hide();
        mainMenu.Show();
    }
    public void ShowCredits()
    {
        HideWholeMenu();
        gameUI.Hide();
        credits.Show();
    }

    public void ShowNextLevel()
    {
        HideWholeMenu();
        gameUI.Hide();
        nextLevel.Show();
    }

    void HideWholeMenu()
    {
        foreach (var x in menuElements)
            x.Hide();
    }
}
