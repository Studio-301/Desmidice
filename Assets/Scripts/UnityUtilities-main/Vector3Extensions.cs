using UnityEngine;

public static class Vector3Extensions
{
    public static Vector3 RotateAroundPivot(this Vector3 point, Vector3 pivot, Quaternion rotation)
    {
        return rotation * (point - pivot) + pivot;
    }
}
