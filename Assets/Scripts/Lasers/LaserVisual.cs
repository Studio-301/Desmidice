using System;
using System.Collections;
using System.Collections.Generic;
using Tools.UnityUtilities;
using UnityEngine;
using UnityEngine.VFX;

public class LaserVisual : MonoBehaviour
{
    [SerializeField] LaserVisualVFX vfx;
    [SerializeField] LaserVisualCheap cheap;
    bool vfxSupported => SystemInfo.supportsComputeShaders && SystemInfo.maxComputeBufferInputsVertex > 0;

    void Awake()
    {
        if (vfxSupported)
            vfx.gameObject.SetActive(true);
        else
            cheap.gameObject.SetActive(true);
    }

    public void SetState(bool state)
    {
        if (vfxSupported)
            vfx.SetState(state);
        else
            cheap.SetState(state);
    }

    public void SetPoints(Vector3 start, Vector3 end, bool endSparks, bool startCap, bool endCap, int strength)
    {
        if (vfxSupported)
            vfx.SetPoints(start, end, endSparks, startCap, endCap, strength);
        else
            cheap.SetPoints(start, end, endSparks, startCap, endCap, strength);
    }

    public void SetColor(Color color)
    {
        if (vfxSupported)
            vfx.SetColor(color);
        else
            cheap.SetColor(color);
    }
}
