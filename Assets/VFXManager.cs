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
    public class BeamElement
    {
        public LaserVisual VFX;
        public UI_BeamValueV2 UI;
    }

    LaserEmitter[] lasers;

    [SerializeField] LaserVisual beamEffect;
    [SerializeField] UI_BeamValueV2 beamUI;
    [SerializeField] Transform uiRoot;

    Pool<BeamElement> elementPool = new()
    {
        ExpansionStep = 20,
        AutoExpand = false
    };

    [SerializeField] float laserDisplayRate;

    void Awake()
    {
        lasers = FindObjectsOfType<LaserEmitter>();

        elementPool.SetFactory(() => new BeamElement()
        {
            VFX = Instantiate(beamEffect, transform),
            UI = Instantiate(beamUI, uiRoot)
        });
        elementPool.OnUse = (x) =>
        {
            x.VFX.SetState(true);
            x.UI.SetState(true);
        };
        elementPool.OnFree = (x) =>
        {
            x.VFX.SetState(false);
            x.UI.SetState(false);
        };
        elementPool.CreateElements(LaserEmitter.MaxSegments * 5);
        elementPool.IterateAllFree((beamElement, i) => {
            var vfx = beamElement.VFX;
            vfx.gameObject.name = $"{nameof(vfx)}_{i}";

            var ui = beamElement.UI;
            ui.gameObject.name = $"{nameof(ui)}_{i}";
        });

        if (laserDisplayRate != 0)
            InvokeRepeating("DisplayLasers", 0, laserDisplayRate);
    }

    private void Update()
    {
        if (laserDisplayRate == 0)
            DisplayLasers();
    }

    void DisplayLasers()
    {
        var totalElementsCount = lasers.Sum(x => Mathf.Clamp(x.LaserBeam.Nodes.Count - 1, 0, int.MaxValue));

        foreach(var laser in lasers)
        {
            var beam = laser.LaserBeam;
            var elemCount = Mathf.Clamp(beam.Nodes.Count - 1, 0, int.MaxValue);

            var recycled = beam.LastElements.Take(elemCount).ToList();
            var recycledCount = recycled.Count;
            var toRender = recycled;
            var newCount = elemCount - toRender.Count();

            for (int i = 0; i < newCount; i++)
            {
                elementPool.GetElement(out var element);
                toRender.Add(element);
            }

            beam.CurrentElements.Clear();
            beam.CurrentElements.AddRange(toRender);

            beam.LastElements.Clear();
            beam.LastElements.AddRange(toRender);

            //Assert
            if (elemCount != beam.CurrentElements.Count)
                Debug.LogError($"[LASER] wanted nodes != vfx count");

            DisplayBeam(beam);
        }

        List<BeamElement> elementsToReturn = new();
        elementPool.IterateAllUsed((element, i) =>
        {
            var notUsed = lasers.All(x => !x.LaserBeam.CurrentElements.Contains(element));
            if (notUsed)
                elementsToReturn.Add(element);
        });
        elementsToReturn.ForEach(x => elementPool.ReturnElement(x));
    }

    public void DisplayBeam(LaserBeam beam)
    {
        var points = beam.Nodes;
        for (int i = 0; i < points.Count - 1; i++)
        {
            var isEnd = i + 2 >= points.Count;
            var a = points[i + 0];
            var b = points[i + 1];

            var middle = a.Point + (b.Point - a.Point) / 2;

            var elem = beam.CurrentElements[i];
            var vfx = elem.VFX;

            //vfx.transform.position = middle;
            vfx.SetPoints(a.Point, b.Point, isEnd, i == 0, a.Strength);
            vfx.SetColor(beam.MainColor);

            var ui = elem.UI;
            ui.Initialize(Camera.main, middle);
            ui.SetValue(a.Strength);
        }
    }
}
