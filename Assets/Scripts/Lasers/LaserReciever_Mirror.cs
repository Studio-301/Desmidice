using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever_Mirror : LaserReciever
{
    public override void Interact(LaserBeam beam, ref Ray ray, RaycastHit info, bool isPreview, out bool continueBeam, AddNode addNode)
    {
        continueBeam = true;
        
        ray.origin = info.point;
        ray.direction = Vector3.Reflect(ray.direction, info.normal);

        addNode(info.point, beam.TotalStrength, this);
    }
}
