using UnityEngine;
using TMPro;

public class UI_2DElement : MonoBehaviour
{
    public Camera Camera;
    public Transform Target;
    public TMP_Text Text;

    public RectTransform This;

    bool initialized;

    public void Initialize(Camera camera, Transform target)
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

    void Update()
    {
        if (initialized)
            This.position = Camera.WorldToScreenPoint(Target.position);
    }
}
