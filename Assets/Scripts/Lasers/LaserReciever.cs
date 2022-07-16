using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever : MonoBehaviour
{
    [Range(1, 6)] public int Number;

    public void Lit(LaserBeam beam)
    {
        beam.TotalStrength += Number;
    }
    public void Preview(LaserBeam beam)
    {
        beam.TotalStrength += Number;
    }
}
