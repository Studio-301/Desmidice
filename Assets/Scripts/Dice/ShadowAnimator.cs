using Shapes;
using System.Collections.Generic;
using UnityEngine;

public class ShadowAnimator : MonoBehaviour
{
    public Rectangle Rectangle;
    public List<Line> Lines;

    void Update()
    {
        Rectangle.DashOffset = Mathf.Repeat(Time.time,1f);

        foreach (Line line in Lines)
        {
            line.DashOffset = Mathf.Repeat(Time.time, 1f);
        }
    }
}
