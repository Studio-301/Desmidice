using DG.Tweening;
using TMPro;
using UnityEngine;

public class DiceValueIndicator : MonoBehaviour
{
    [Header("References")]
    public Dice Dice;
    public Manipulatable Manipulatable;

    [Header("Texts")]
    public TMP_Text A;
    public TMP_Text B;
    public TMP_Text D;
    public TMP_Text E;

    [Header("Shadows")]
    public GameObject AShadow;
    public GameObject BShadow;
    public GameObject DShadow;
    public GameObject EShadow;

    [Header("Tweens")]
    public CanvasGroup CanvasGroup;
    public float FadeLength = 0.3f;
    public float FadeOutDelay = 1f;

    Camera camera;

    bool isHeld = false;

    void Start()
    {
        camera = Camera.main;

        Manipulatable.OnGrab += OnGrab;
        Manipulatable.OnRelease += OnRelease;
        Manipulatable.OnRotate += OnRotate;

        CanvasGroup.alpha = 0f;
    }

    void OnGrab()
    {
        isHeld = true;

        UpdateValues();

        CanvasGroup.DOComplete();
        CanvasGroup.DOFade(1f, FadeLength);
    }

    void OnRelease()
    {
        isHeld = false;

        CanvasGroup.DOComplete();
        CanvasGroup.DOFade(0f, FadeLength);
    }

    void OnRotate()
    {
        if (!isHeld)
        {
            UpdateValues();

            CanvasGroup.DOComplete();
            CanvasGroup.DOFade(1f, FadeLength);
            CanvasGroup.DOFade(0f, FadeLength).SetDelay(FadeOutDelay);
        }
    }

    void Update()
    {
        CanvasGroup.transform.eulerAngles = new Vector3(90f, Manipulatable.ModelRoot.eulerAngles.y, 0);

        RotateTransformTowardsCamera(A.transform);
        RotateTransformTowardsCamera(B.transform);
        RotateTransformTowardsCamera(D.transform);
        RotateTransformTowardsCamera(E.transform);
    }

    void RotateTransformTowardsCamera(Transform thing)
    {
        thing.eulerAngles = new Vector3(90f, camera.transform.parent.eulerAngles.y, 0);
    }

    void UpdateValues()
    {
        A.text = GetValueText(Dice.data.A);
        B.text = GetValueText(Dice.data.B);
        D.text = GetValueText(Dice.data.D);
        E.text = GetValueText(Dice.data.E);

        AShadow.SetActive(A.text != "");
        BShadow.SetActive(B.text != "");
        DShadow.SetActive(D.text != "");
        EShadow.SetActive(E.text != "");
    }

    string GetValueText(Dice.Sides e)
    {
        string output = ((int)e).ToString();
        return output == "0" ? "" : output;
    }
}
