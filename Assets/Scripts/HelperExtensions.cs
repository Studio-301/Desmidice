using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperExtensions
{
    //FLOAT
    public static float Remap(this float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
    
    public static float Clamp(this float value, float min, float max)
    {
        return Mathf.Clamp(value, min, max);
    }
    
    
    //VECTOR3
    public static Vector3 NoY(this Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
}
