using TMPro;
using UnityEngine;

public class UI_BeamValueV2 : MonoBehaviour
{
    public TMP_Text Value;

    Camera camera;
    bool isOn;

    public void Initialize(Camera main, Vector3 middle)
    {
        camera = main;
        transform.position = middle;
        transform.position += Vector3.up * 0.5f;
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
        isOn = state;
    }

    public void SetValue(int value)
    {
        Value.text = value.ToString();
    }

    void Update()
    {
        if (isOn)
            transform.eulerAngles = new Vector3(90f, camera.transform.parent.eulerAngles.y, 0);
    }
}
