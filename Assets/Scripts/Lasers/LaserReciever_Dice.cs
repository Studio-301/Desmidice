using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever_Dice : LaserReciever
{
    public override bool Reflect => true;

    [Range(1, 6)] public int Number;

    public override void Lit(LaserBeam beam)
    {
        beam.TotalStrength += Number;
    }
    public override void Preview(LaserBeam beam)
    {
        beam.TotalStrength += Number;
    }
}
