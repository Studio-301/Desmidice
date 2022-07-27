using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGrid : MonoBehaviour
{
    [SerializeField] Transform grid;
    [SerializeField] MeshRenderer gridRenderer;

    [Title("Dimentions")]
    [SerializeField] int width;
    [SerializeField] int height;

    [Title("Offset")]
    [SerializeField] int offsetX;
    [SerializeField] int offsetY;
    [SerializeField] float offsetDepth;

    [ContextMenu("Initialize")]
    private void Awake()
    {
        grid.transform.localScale = new Vector3(width, height);
        grid.transform.position = new Vector3(offsetX, offsetDepth, offsetY);

        gridRenderer.material = Instantiate(gridRenderer.material);
        gridRenderer.material.mainTextureScale = new Vector2(width, height);
    }
}
