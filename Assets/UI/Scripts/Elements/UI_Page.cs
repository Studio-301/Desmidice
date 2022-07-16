using UnityEngine;
using DG.Tweening;

public class UI_Page : MonoBehaviour
{
    public float FadeLength = 0.2f;
    public Ease ShowEase = Ease.InOutSine;
    public Ease HideEase = Ease.InOutSine;
    public bool IsHiddenOnStart = true;

    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = IsHiddenOnStart ? 0f : 1f;
        canvasGroup.interactable = !IsHiddenOnStart;
        canvasGroup.blocksRaycasts = !IsHiddenOnStart;
    }

    public void Show()
    {
        canvasGroup.DOFade(1f, FadeLength).SetEase(ShowEase);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public void Hide()
    {
        canvasGroup.DOFade(0f, FadeLength).SetEase(HideEase);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
