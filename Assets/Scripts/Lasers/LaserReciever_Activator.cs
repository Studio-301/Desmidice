using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReciever_Activator : LaserReciever
{
    public override bool Reflect => false;

    [Title("Events")]
    public UnityEvent Activate;
    public UnityEvent Deactivate;

    [Title("Settings")]
    public int Condition;

    [Disable] public bool IsCompleate;
    bool lastApplied;

    bool updateDecay;

    void Awake()
    {
        Deactivate?.Invoke();
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
        if (updateDecay)
            IsCompleate = false;

        if (lastApplied != IsCompleate)
        {
            lastApplied = IsCompleate;
            if (IsCompleate)
                Activate.Invoke();
            else
                Deactivate.Invoke();
        }

        updateDecay = true;
    }
}
