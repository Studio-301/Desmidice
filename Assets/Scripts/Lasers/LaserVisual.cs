using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LaserVisual : MonoBehaviour
{
    public VisualEffect laserVFX;

    public void SetPoints(Vector3 start, Vector3 end, bool endSparks)
    {
        laserVFX.SetVector3("A", start);
        laserVFX.SetVector3("B", end);
        laserVFX.SetBool("End Sparks", endSparks);
    }
    
    public void SetColor(Color color)
    {
        laserVFX.SetVector4("Color", color);
    }
}
