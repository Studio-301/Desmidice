using UnityEngine;
using DG.Tweening;

public class Manipulatable : MonoBehaviour
{
    [Header("References")]
    public Collider Collider;

    [Header("Rotation")]
    public bool EnableRotation = true;
    public float RotationIncrement = 90f;
    public float RotationLength = 0.3f;
    public Ease RotationEase = Ease.OutBounce;

    [Header("Move")]
    public bool EnableMove = true;
    public float MoveLength = 0.3f;
    public Ease MoveEase = Ease.OutBounce;

    public void Rotate()
    {
        if (!EnableRotation)
            return;

        transform.DOComplete();
        transform.DOLocalRotate(new Vector3(0, transform.localEulerAngles.y + RotationIncrement, 0), RotationLength)
                 .SetEase(RotationEase);
    }

    public void SnapTo(Vector3 destination)
    {
        if (!EnableMove)
            return;

        destination.y = transform.position.y;

        transform.DOComplete();
        transform.DOMove(destination, MoveLength).SetEase(MoveEase);
    }

    public void MoveTo(Vector3 destination)
    {
        if (!EnableMove)
            return;

        destination.y = transform.position.y;
        transform.position = destination;
    }

    public void SetColliderState(bool state)
    {
        Collider.enabled = state;
    }
}
