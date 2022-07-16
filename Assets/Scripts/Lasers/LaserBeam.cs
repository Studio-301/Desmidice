using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void AddNode(Vector3 pos, int strength, LaserReciever reciever);
public class LaserBeam
{
    /// <summary>
    /// Is laser ending in a reciever with valid condition
    /// </summary>
    public bool Complete; 

    /// <summary>
    /// Total value of the beam
    /// </summary>
    public int TotalStrength;

    /// <summary>
    /// Laser path
    /// </summary>
    public List<Node> Nodes = new List<Node>(80);

    public void Reset()
    {
        TotalStrength = 0;
        Nodes.Clear();
        Complete = false;
    }

    /// <summary>
    /// Uses current strength.
    /// </summary>
    public void AddNode(Vector3 pos, LaserReciever reciever) => AddNode(pos, TotalStrength, reciever);
    public void AddNode(Vector3 pos, int strength, LaserReciever reciever) => Nodes.Add(new Node()
    {
        Point = pos,
        Strength = strength,
        Reciever = reciever
    });

    public struct Node
    {
        public Vector3 Point;
        public int Strength;
        public LaserReciever? Reciever;
    }
}
