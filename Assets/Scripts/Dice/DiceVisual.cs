using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceVisual : MonoBehaviour
{
    [Header("References")]
    public Material diceMaterial;

    public MeshRenderer meshRenderer;

    Material diceMaterialInstance;

    void Awake()
    {
        diceMaterialInstance = new Material(diceMaterial);
            
        meshRenderer.material = diceMaterialInstance;
    }

    public void SetColors(Color baseColor, Color dotColor)
    {
        diceMaterialInstance.SetColor("_Dice_Color", baseColor);
        diceMaterialInstance.SetColor("_Dot_Color", dotColor);
    }

    public void SetValues(int a, int b, int c, int d, int e, int f)
    {
        diceMaterialInstance.SetInt("_Side_A", a);
        diceMaterialInstance.SetInt("_Side_B", b);
        diceMaterialInstance.SetInt("_Side_C", c);
        diceMaterialInstance.SetInt("_Side_D", d);
        diceMaterialInstance.SetInt("_Side_E", e);
        diceMaterialInstance.SetInt("_Side_F", f);
    }
}
