using UnityEngine;
using UnityEngine.UI;

public class UI_MuteButton : MonoBehaviour
{
    public Image OnIcon;
    public Image OffIcon;

    bool state = true;

    public void OnButtonClicked()
    {
        state = !state;

        OnIcon.gameObject.SetActive(state);
        OffIcon.gameObject.SetActive(!state);

        AudioListener.volume = state ? 1f : 0f;
    }
}
