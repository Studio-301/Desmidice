using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReciever_Activator : LaserReciever
{
    [Title("Events")]
    public UnityEvent Activate;
    public UnityEvent Deactivate;

    [Title("Settings")]
    [SerializeField] bool matchAnyNumber;
    [HideIf("matchAnyNumber", true)]
    public int Condition;

    [Disable] public bool IsCompleate;
    bool lastApplied;

    bool updateDecay;

    [SerializeField] UI_BeamValueV2 ui;

    public override LaserReciever_Settings Settings => new()
    {
        HideStartCap = false,
        HideEndCap = true
    };

    void Awake()
    {
        ui.Initialize(Camera.main, ui.transform.position);
        ui.SetState(true);

        Activate.AddListener(() =>
        {
            ui.Value.text = $"DONE";
        });
        Deactivate.AddListener(() =>
        {
            ui.Value.text = $"{Condition}";
        });

        Deactivate?.Invoke();
    }

    void LateUpdate() => HandleEvents();

    public override void Interact(LaserBeam beam, ref Ray ray, RaycastHit info, bool isPreview, out bool continueBeam, AddNode addNode)
    {
        continueBeam = false;
        IsCompleate = beam.Complete = beam.TotalStrength == Condition || matchAnyNumber;
        addNode(info.point, beam.TotalStrength, this);
        updateDecay = false;
    }
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
