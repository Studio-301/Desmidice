using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterVisual : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform claws;
    [SerializeField] List<MeshRenderer> meshRenderers;
    [SerializeField] Material material;
    
    [Header("Rotation")]
    [SerializeField] float speed = 1;

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
        materialInstance.SetColor("_EmissionColor", color);
    }
}
