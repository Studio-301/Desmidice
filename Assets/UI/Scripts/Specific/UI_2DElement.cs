using UnityEngine;

public class UI_2DElement : MonoBehaviour
{
    public Camera Camera;
    public Transform Target;

    public RectTransform This;

    bool initialized;

    public void Initialize(Camera camera, Transform target)
    {
        Camera = camera;
        Target = target;

        This = GetComponent<RectTransform>();

        initialized = true;
    }

    void Update()
    {
        if (initialized)
            This.position = Camera.WorldToScreenPoint(Target.position);
    }
}
