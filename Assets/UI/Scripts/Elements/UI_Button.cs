using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public RectTransform Underline;
    public TMP_Text Text;

    public float AnimationLength;
    public Ease LineEase;

    void Start()
    {
        Underline.localScale = new Vector3(0, 1, 1);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Underline.DOScaleX(1f, AnimationLength).SetEase(LineEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Underline.DOScaleX(0f, AnimationLength).SetEase(LineEase);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SoundBank.Instance.PlayClip("UIClick");
    }
}
