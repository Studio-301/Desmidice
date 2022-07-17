using System;
using System.Collections;
using System.Collections.Generic;
using Tools.UnityUtilities;
using UnityEngine;
using UnityEngine.VFX;

public class LaserVisual : MonoBehaviour
{
    public VisualEffect laserVFX;

    public void SetState(bool state)
    {
        if (laserVFX.enabled != state)
            laserVFX.enabled = state;
    }

    public void SetPoints(Vector3 start, Vector3 end, bool endSparks, bool startCap, bool endCap, int strength)
    {
        transform.position = start + (end - start) / 2;
        laserVFX.SetVector3("A", start);
        laserVFX.SetVector3("B", end);
        laserVFX.SetBool("End Sparks", endSparks);
        laserVFX.SetInt("Number", Mathf.Clamp(strength, 0, 9));
        laserVFX.SetBool("Is Start", startCap);
        laserVFX.SetBool("Is End", endCap);
    }
    
    public void SetColor(Color color)
    {
        laserVFX.SetVector4("Color", color);
    }
}
