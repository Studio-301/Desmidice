using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserReciever : MonoBehaviour
{
    public abstract bool Reflect { get; }
    public abstract void Lit(LaserBeam beam);
    public abstract void Preview(LaserBeam beam);
}
