using UnityEngine;
using TMPro;

public class UI_2DElement : MonoBehaviour
{
    public Camera Camera;
    public Vector3 Target;
    public TMP_Text Text;

    public RectTransform This;

    [SerializeField] CanvasGroup group;

    bool initialized;

    public void Initialize(Camera camera, Vector3 target)
    {
        Camera = camera;
        Target = target;

        This = GetComponent<RectTransform>();

        initialized = true;
    }

    public void SetValue(int value)
    {
        Text.text = value.ToString();
    }

    public void SetState(bool state)
    {
        group.alpha = state ? 1 : 0;

        if (!state)
            initialized = false;
    }

    void Update()
    {
        if (initialized)
            This.position = Camera.WorldToScreenPoint(Target);
    }
}
