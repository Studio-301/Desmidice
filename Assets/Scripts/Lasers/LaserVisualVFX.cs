using UnityEngine;
using UnityEngine.VFX;

public class LaserVisualVFX : MonoBehaviour
{
    public VisualEffect LaserVFX;

    public void SetState(bool state)
    {
        if (LaserVFX.enabled != state)
            LaserVFX.enabled = state;
    }

    public void SetPoints(Vector3 start, Vector3 end, bool endSparks, bool startCap, bool endCap, int strength)
    {
        transform.position = start + (end - start) / 2;
        LaserVFX.SetVector3("A", start);
        LaserVFX.SetVector3("B", end);
        LaserVFX.SetBool("End Sparks", endSparks);
        LaserVFX.SetInt("Number", Mathf.Clamp(strength, 0, 9));
        LaserVFX.SetBool("Is Start", startCap);
        LaserVFX.SetBool("Is End", endCap);
    }

    public void SetColor(Color color)
    {
        LaserVFX.SetVector4("Color", color);
    }
}