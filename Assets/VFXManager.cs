using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools.UnityUtilities;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.VFX;

[DefaultExecutionOrder(-1)]
public class VFXManager : MonoBehaviour
{
    [SerializeField] LaserEmitter[] lasers;

    [SerializeField] LaserVisual beamEffect;

    Pool<LaserVisual> vfxPool = new()
    {
        ExpansionStep = 20,
        AutoExpand = false
    };

    [SerializeField] float laserDisplayRate;

    void Awake()
    {
        vfxPool.SetFactory(() => Instantiate(beamEffect, transform));
        vfxPool.OnUse = (x) => x.SetState(true);
        vfxPool.OnFree = (x) => x.SetState(false);
        vfxPool.CreateElements(LaserEmitter.MaxSegments * 5);

        InvokeRepeating("DisplayLasers", 0, laserDisplayRate);
    }

    void DisplayLasers()
    {
        var totalElementsCount = lasers.Sum(x => Mathf.Clamp(x.LaserBeam.Nodes.Count - 1, 0, int.MaxValue));
        var elements = vfxPool.RecyclePool(totalElementsCount, fullReinit: false);
        if (elements.GroupBy(x => x).Any(x => x.Count() > 1))
        {
            string status = "";

            status += "USED:\n";
            vfxPool.IterateAllUsed((e, i) => status += $"{i} - {e.GetHashCode()}\n");
            status += "\n\nFREE:\n";
            vfxPool.IterateAllFree((e, i) => status += $"{i} - {e.GetHashCode()}\n");

            //Debug.Log($"POOL:\n{status}");

            Debug.LogError($"Duplicit results detected:\n{status}");
        }

        int vfxIndex = 0;
        foreach(var x in lasers)
        {
            DisplayBeam(x.LaserBeam, () => elements[vfxIndex++]);
        }
    }

    public void DisplayBeam(LaserBeam beam, Func<LaserVisual> getElement)
    {
        var points = beam.Nodes;
        for (int i = 0; i < points.Count - 1; i++)
        {
            var isEnd = i + 1 >= points.Count;
            var a = points[i + 0];
            var b = points[i + 1];

            var middle = a.Point + (b.Point - a.Point) / 2;

            var vfx = getElement();

            vfx.SetPoints(a.Point, b.Point, isEnd);
        }
    }
}
