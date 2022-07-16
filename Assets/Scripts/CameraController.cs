using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float time = 0.25f;
    [SerializeField] Ease ease;

    [Disable]
    [SerializeField] float targetAngle = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Rotate(-90);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Rotate(+90);
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
