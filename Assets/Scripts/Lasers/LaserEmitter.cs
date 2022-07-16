using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitter : MonoBehaviour
{
    public bool IsLit { get; set; }

    public LaserBeam LaserBeam { get; private set; } = new();

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

        var t = orientationReference;

        Vector3 origin = t.position;
        Vector3 dir = t.forward;

        LaserBeam.AddNode(origin);

        //Solve beam
        bool forceStop = false;
        for (int i = 0; i < MaxSegments && !forceStop; i++)
        {
            if (Raycast(origin, dir, out info))
            {
                var obj = info.collider.gameObject;


                //Valid result - loop will continue
                if (obj.TryGetComponent<LaserReciever>(out var reciever))
                {
                    if (preview)
                        reciever.Preview(LaserBeam);
                    else
                        reciever.Lit(LaserBeam);
                }
                else
                    forceStop = true;

                //Params for next segment
                origin = info.point;
                dir = Vector3.Reflect(dir, info.normal);

                LaserBeam.AddNode(info.point);
            }
            else
                break;
        }
    }


    bool Raycast(Vector3 start, Vector3 dir, out RaycastHit hit) => Physics.Raycast(start, dir, out hit, 999, mask);

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

    void Preview() => TickLaser(preview: true);

    void OnDrawGizmos() //Debug
    {
        if (LaserBeam == null)
            return;

        var points = LaserBeam.Nodes;
        for (int i = 0; i < points.Count - 1; i++)
        {
            var a = points[i + 0];
            var b = points[i + 1];

            var val = a.Strength / 18f;
            Gizmos.color = new Color(1 - val, .1f + val, val);

#if UNITY_EDITOR
            var middle = a.Point + (b.Point - a.Point) / 2;
            UnityEditor.Handles.Label(middle, $"{a.Strength}");
#endif

            Gizmos.DrawLine(a.Point, b.Point);
        }
    }
}
