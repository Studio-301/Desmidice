using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReciever : MonoBehaviour
{
    [SerializeField] LaserSource[] observers;

    public bool IsLit { get; set; }

    public void Solve()
    {
        foreach(var x in observers)
        {
            x.IsLit = IsLit;
        }
    }
}
