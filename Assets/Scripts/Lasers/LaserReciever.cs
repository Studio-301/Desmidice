using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever_Settings
{
    public bool HideEndCap;
    public bool HideStartCap;
}

public abstract class LaserReciever : MonoBehaviour
{
    public abstract LaserReciever_Settings Settings {get;}
    public abstract void Interact(LaserBeam beam, ref Ray ray, RaycastHit info, bool isPreview, out bool continueBeam, AddNode addNode);
}
