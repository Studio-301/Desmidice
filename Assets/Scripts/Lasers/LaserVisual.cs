using System;
using System.Collections;
using System.Collections.Generic;
using Tools.UnityUtilities;
using UnityEngine;
using UnityEngine.VFX;

public class LaserVisual : MonoBehaviour, IPoolable
{
    public VisualEffect laserVFX;

    public bool IsActive { get; set; }
    public Action RequestReturn { get; set; }

    public void SetState(bool state)
    {
        if (laserVFX.enabled != state)
        {
            laserVFX.enabled = state;
            if (state)
                laserVFX.Reinit();
        }

        if (state && !IsActive)
            Debug.LogError("Odd sate: turning on dead instance");
    }

    public void SetPoints(Vector3 start, Vector3 end, bool endSparks, int strength)
    {
        transform.position = start + (end - start) / 2;
        laserVFX.SetVector3("A", start);
        laserVFX.SetVector3("B", end);
        laserVFX.SetBool("End Sparks", endSparks);
        laserVFX.SetInt("Number", Mathf.Clamp(strength, 0, 9));
    }
    
    public void SetColor(Color color)
    {
        laserVFX.SetVector4("Color", color);
    }

    public void Initialize() { }

    public void OnFree() { }

    public void OnUse() { }
}
