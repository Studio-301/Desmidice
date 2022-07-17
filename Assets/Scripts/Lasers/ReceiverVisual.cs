using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform claws;
    [SerializeField] List<MeshRenderer> meshRenderers;
    [SerializeField] Material material;

    [Header("Rotation")]
    [SerializeField] float speed = 50;

    Material materialInstance;

    void Awake()
    {
        materialInstance = new Material(material);

        foreach (var meshRenderer in meshRenderers)
        {
            var materials = meshRenderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i].name == "Props Emission")
                {
                    materials[i] = materialInstance;
                }
            }
            meshRenderer.materials = materials;
        }
    }

    void Update()
    {
        claws.Rotate(0, Time.deltaTime * speed, 0);
    }

    public void SetColor(Color color)
    {
        materialInstance.SetColor("_EMISSION", color);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Valid()
    {
        SetSpeed(500);
        SetColor(Color.white);
    }

    public void Invalid()
    {
        SetSpeed(50);
        SetColor(Color.black);
    }

}
