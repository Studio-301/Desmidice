using UnityEngine;

public static class TransformExtensions
{
    public static RectTransform RectTransform(this Transform transform)
    {
        return (RectTransform)transform;
    }
}
