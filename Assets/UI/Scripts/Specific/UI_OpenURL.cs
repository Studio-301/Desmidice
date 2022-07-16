using UnityEngine;

public class UI_OpenURL : MonoBehaviour
{
    public string URL;

    public void OnButtonClicked()
    {
        Application.OpenURL(URL);
    }
}
