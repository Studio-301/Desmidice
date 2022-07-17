using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] Transform cam;

    [Header("Rotate")]
    [SerializeField] float time = 0.25f;
    [SerializeField] Ease ease;

    [Disable]
    [SerializeField] float targetAngle = 0;


    [Header("Zoom")]
    [SerializeField] float zoomSensitivity = 2f;
    [SerializeField] float movementLerp = 15f;
    [SerializeField] Vector2 zoomLimits = new Vector2(0, 20);

    float targetZoom = 0;
    Vector3 camDir => Quaternion.Euler(0, -targetAngle, 0) * cam.forward;

    [Header("Pan")]
    [SerializeField] float panSensitivity = 20f;
    
    Vector3 panForward => Quaternion.Euler(0, -targetAngle, 0) * transform.forward.NoY().normalized;
    Vector3 panRight => Quaternion.Euler(0, -targetAngle, 0) * transform.right.NoY().normalized;

    Vector3 camBasePos;

    void Awake()
    {
        camBasePos = cam.localPosition;
    }

    void Update()
    {
        //ROTATE
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Rotate(-90);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Rotate(+90);

        //ZOOM
        targetZoom += Input.mouseScrollDelta.y * zoomSensitivity;
        targetZoom = Mathf.Clamp(targetZoom, zoomLimits.x, zoomLimits.y);

        //PAN
        //More zoomed in => More panning   
        float panIntensity = Mathf.InverseLerp(zoomLimits.x, zoomLimits.y, targetZoom) * panSensitivity;
        
        Vector3 mousePos = Input.mousePosition;
        
        float xNormalized = (mousePos.x / Screen.width).Remap(0.2f, 0.8f, -1, 1).Clamp(-1, 1);
        float yNormalized = (mousePos.y / Screen.height).Remap(0.2f, 0.8f, -1, 1).Clamp(-1, 1);

        cam.localPosition = Vector3.Lerp(cam.localPosition, camBasePos
                                                            + targetZoom * camDir
                                                            + (xNormalized * panIntensity) * panRight
                                                            + (yNormalized * panIntensity) * panForward
                                                            , Time.deltaTime * movementLerp);
    }

    void Rotate(float diffAngle)
    {
        targetAngle += diffAngle;

        transform.DOKill();
        //transform.DORotate(Vectior3.up * dffAngle, time, RotateMode.WorldAxisAdd).SetEase(ease);

        var quternion = Quaternion.AngleAxis(targetAngle, Vector3.up);

        transform.DORotateQuaternion(quternion, time).SetEase(ease);
    }
}
