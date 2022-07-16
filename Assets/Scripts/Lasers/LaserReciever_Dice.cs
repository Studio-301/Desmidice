using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever_Dice : LaserReciever_Mirror
{
    [Range(1, 6)] public int Number;

    public override void Interact(LaserBeam beam, ref Ray ray, RaycastHit info, bool isPreview, out bool continueBeam, AddNode addNode)
    {
        beam.TotalStrength += Number;

        base.Interact(beam, ref ray, info, isPreview, out continueBeam, addNode);
    }
}
