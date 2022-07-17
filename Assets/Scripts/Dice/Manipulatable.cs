using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class Manipulatable : MonoBehaviour
{
    [Header("References")]
    public Collider Collider;
    public Transform LaserColliderRoot;
    public Transform ModelRoot;
    public GameObject Shadow;

    [SerializeField] List<LaserReciever_Dice> diceSides;

    [Header("Rotation")]
    public bool EnableRotation = true;
    public float RotationIncrement = 90f;
    public float RotationLength = 0.3f;
    public Ease RotationEase = Ease.OutBounce;

    [Header("Move")]
    public bool EnableMove = true;
    public float MoveLength = 0.3f;
    public Ease MoveEase = Ease.OutBounce;

    [Header("Grab")]
    public float GroundY = 0.7f;
    public float SkyY = 2.2f;
    public float GrabLength = 0.3f;
    public Ease GrabEase = Ease.OutBack;
    public Ease ReleaseEase = Ease.OutBounce;

    [Header("Callbacks")]
    public Action OnGrab;
    public Action OnRelease;
    public Action OnRotate;

    float currentRotation = 0f;

    void Start()
    {
        Shadow.SetActive(false);

        currentRotation = ModelRoot.localEulerAngles.y;

        transform.position = new Vector3(transform.position.x, GroundY, transform.position.z);
    }

    public void Rotate()
    {
        if (!EnableRotation)
            return;

        currentRotation += RotationIncrement;
        currentRotation = Mathf.Repeat(currentRotation, 360f);

        diceSides.ForEach(x => x.IsReflective = false);
        ModelRoot.DOLocalRotate(new Vector3(0, currentRotation, 0), RotationLength)
                 .SetEase(RotationEase)
                 .onComplete += () => diceSides.ForEach(x => x.IsReflective = true);

        LaserColliderRoot.localRotation = Quaternion.AngleAxis(currentRotation, Vector3.up);

        SoundBank.Instance.PlayClip("Rotate", transform.position);

        OnRotate?.Invoke();
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

    public void MoveShadowTo(Vector3 destination)
    {
        Shadow.transform.position = destination;
    }

    public void Grab()
    {
        Shadow.SetActive(true);

        Collider.enabled = false;
        transform.DOMoveY(SkyY, GrabLength).SetEase(GrabEase);

        OnGrab?.Invoke();

        SoundBank.Instance.PlayClip("Grab", transform.position);
    }

    public void Release()
    {
        Shadow.SetActive(false);

        Collider.enabled = true;
        transform.DOMoveY(GroundY, GrabLength).SetEase(ReleaseEase);

        OnRelease?.Invoke();

        SoundBank.Instance.PlayClip("Drop", transform.position);
    }
}
