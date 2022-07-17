using UnityEngine;
using UnityEngine.UI;

public class UI_MuteButton : MonoBehaviour
{
    public Image OnIcon;
    public Image OffIcon;

    bool state = true;

    private void Awake()
    {
        AudioListener.volume = 0.7f;
    }

    public void OnButtonClicked()
    {
        state = !state;

        OnIcon.gameObject.SetActive(state);
        OffIcon.gameObject.SetActive(!state);

        AudioListener.volume = state ? 0.7f : 0f;
    }
}
