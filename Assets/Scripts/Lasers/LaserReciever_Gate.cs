using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever_Gate : LaserReciever
{
    public override LaserReciever_Settings Settings => new()
    {
        HideEndCap = false,
        HideStartCap = false
    };

    [Title("Gate")]
    [SerializeField] bool xFlip = true;
    [SerializeField] Transform entrance;
    [SerializeField] Transform exit;

    public override void Interact(LaserBeam beam, ref Ray ray, RaycastHit info, bool isPreview, out bool continueBeam, AddNode addNode)
    {
        continueBeam = true;

        var dir = exit.position - entrance.position;
        dir.Normalize();

        //var newOrigin = info.point + dir;

        //var entrancePlane = new Plane(entrance.forward, entrance.position);
        //var exitPlane = new Plane(exit.forward, exit.position);

        //TO LOCAL SPACE
        var entranceHit = entrance.InverseTransformPoint(info.point);
        entranceHit.z = 0; //remove depth from the projection
        
        if(xFlip)
            entranceHit.x *= Vector3.Dot(entrance.forward, exit.forward) < 0? -1 : 1; //Flip projection
        

        var entranceDir = entrance.InverseTransformDirection(ray.direction);

        //TO WORLD SPACE based on different parent(Transform)
        var exitHit = exit.TransformPoint(entranceHit);
        var exitDir = exit.TransformPoint(entranceDir);

        ray.origin = exitHit;
        ray.direction = exit.forward;

        addNode(info.point, beam.TotalStrength, this);
        addNode(ray.origin, beam.TotalStrength, this);
    }
}
