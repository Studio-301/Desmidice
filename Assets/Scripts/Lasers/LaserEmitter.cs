using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public bool IsLit { get; set; }

    public LaserBeam LaserBeam { get; private set; } = new();
    [SerializeField] int BaseBeamStreangth;

    [SerializeField] LayerMask mask;
    [EditorButton("ClearPreview", "Hide laser")]
    [EditorButton("Enable_Preview", "Preview laser")]
    [SerializeField] Transform orientationReference;

    const int MaxSegments = 100;

    RaycastHit info;

    void Awake()
    {
        LaserBeam.Reset();
    }

    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.F5))
        TickLaser();
    }

    public void TickLaser(bool preview = false)
    {
        if (LaserBeam == null)
            throw new System.ArgumentNullException($"Beam is null.");

        //Reset beam
        LaserBeam.Reset();
        LaserBeam.TotalStrength = BaseBeamStreangth;

        var t = orientationReference;

        var ray = new Ray(t.position, t.forward);

        LaserBeam.AddNode(ray.origin, null);

        //Solve beam
        bool forceStop = false;
        for (int i = 0; i < MaxSegments && !forceStop; i++)
        {
            if (Raycast(ray, out info))
            {
                var obj = info.collider.gameObject;

                if (obj.TryGetComponent<LaserReciever>(out var reciever))
                {
                    reciever.Interact(LaserBeam, ref ray, info, preview, out bool continueBeam, LaserBeam.AddNode);
                    forceStop = !continueBeam;
                }
                else
                {
                    forceStop = true;
                    LaserBeam.AddNode(info.point, null);
                }
            }
            else
                forceStop = true;
        }
    }


    bool Raycast(Ray ray, out RaycastHit hit) => Physics.Raycast(ray.origin, ray.direction, out hit, 999, mask);

    // -------------------- DEBUG
    void ClearPreview()
    {
        LaserBeam?.Reset();
        Disable_Preview();
    }

    void Enable_Preview()
    {
#if UNITY_EDITOR
        Disable_Preview();
        UnityEditor.EditorApplication.update += Preview;
#endif
    }
    void Disable_Preview()
    {
#if UNITY_EDITOR
        for (int i = 0; i < 100; i++)
            UnityEditor.EditorApplication.update -= Preview;
#endif
    }

    void Preview()
    {
        if (gameObject == null)
            return;

        TickLaser(preview: true);
    }

    [SerializeField, Range(0, 20)] int manualPreview;
    void OnDrawGizmos() //Debug
    {
        if (LaserBeam == null)
            return;

        var points = LaserBeam.Nodes;
        for (int i = 0; i < points.Count - 1; i++)
        {
            if (manualPreview != 0 && i >= manualPreview)
                continue;

            var a = points[i + 0];
            var b = points[i + 1];

            var val = a.Strength / 18f;
            Gizmos.color = new Color(1 - val, .1f + val, val);

#if UNITY_EDITOR
            var middle = a.Point + (b.Point - a.Point) / 2;
            UnityEditor.Handles.Label(middle, $"{a.Strength} - {a.Reciever?.gameObject.name}");
#endif

            Gizmos.DrawLine(a.Point, b.Point);
        }

        if(LaserBeam.Complete && points.Any())
        {
            var pos = points.Last().Point;
#if UNITY_EDITOR
            UnityEditor.Handles.Label(pos, $"COMPLEATED: {LaserBeam.TotalStrength}");
#endif    
        }
    }
}
