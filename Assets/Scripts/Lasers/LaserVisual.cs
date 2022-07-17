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
#if UNITY_WEBGL
        cheap.gameObject.SetActive(true);
#else
            vfx.gameObject.SetActive(true);
#endif
    }

    public void SetState(bool state)
    {
#if UNITY_WEBGL
        cheap.SetState(state);
#else
            vfx.SetState(state);
#endif
    }

    public void SetPoints(Vector3 start, Vector3 end, bool endSparks, bool startCap, bool endCap, int strength)
    {
#if UNITY_WEBGL
        cheap.SetPoints(start, end, endSparks, startCap, endCap, strength);
#else
            vfx.SetPoints(start, end, endSparks, startCap, endCap, strength);
#endif
    }

    public void SetColor(Color color)
    {
#if UNITY_WEBGL
        cheap.SetColor(color);
#else
            vfx.SetColor(color);
#endif
    }
}
