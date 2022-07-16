using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReciever_Activator : LaserReciever
{
    public override bool Reflect => false;

    [Title("Events")]
    public UnityEvent Activate;
    public UnityEvent Deactiavte;

    [Title("Settings")]
    public int Condition;

    [Disable] public bool IsCompleate;
    bool isCompleateLastTick;
    bool updateDecay;

    void Awake()
    {
        Deactiavte?.Invoke();
    }

    public override void Lit(LaserBeam beam)
    {
        IsCompleate = beam.Complete = beam.TotalStrength == Condition;
        updateDecay = false;
    }

    public override void Preview(LaserBeam beam)
    {
        IsCompleate = beam.Complete = beam.TotalStrength == Condition;
        updateDecay = false;
    }

    void LateUpdate() => HandleEvents();

    void HandleEvents()
    {
        if (updateDecay) //wasn't updated for at least one frame
            IsCompleate = false;

        if (isCompleateLastTick != IsCompleate)
        {
            if (IsCompleate)
                Activate.Invoke();
            else
                Deactiavte.Invoke();
        }

        isCompleateLastTick = IsCompleate;

        updateDecay = true;
    }
}
