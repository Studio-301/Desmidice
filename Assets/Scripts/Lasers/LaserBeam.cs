using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    public int TotalStrength;
    public List<Node> Nodes = new List<Node>(80);

    public void Reset()
    {
        TotalStrength = 0;
        Nodes.Clear();
    }

    /// <summary>
    /// Uses current strength.
    /// </summary>
    public void AddNode(Vector3 pos) => AddNode(pos, TotalStrength);
    public void AddNode(Vector3 pos, int strength) => Nodes.Add(new Node()
    {
        Point = pos,
        Strength = strength,
    });

    public struct Node
    {
        public Vector3 Point;
        public int Strength;
    }
}
