using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LaserReciever : MonoBehaviour
{
    public abstract void Interact(LaserBeam beam, ref Ray ray, RaycastHit info, bool isPreview, out bool continueBeam, AddNode addNode);
}
