using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public enum Sides { _0, _1, _2, _3, _4, _5, _6, _7, _8, _9, _10 }

    [Title("Reference")]
    [SerializeField] DiceVisual visuals;

    [Title("Settings")]
    public DiceData data;
    [EditorButton("Reinitialize", activityType: ButtonActivityType.OnPlayMode)]
    [SerializeField] DiceColliders colliders;

    void Awake()
    {
        colliders.A.Number = (int)data.A;
        colliders.B.Number = (int)data.B;
        colliders.C.Number = (int)data.C;
        colliders.D.Number = (int)data.D;
        colliders.E.Number = (int)data.E;
        colliders.F.Number = (int)data.F;
    }

    void Start() => visuals.SetValues((int)data.A, (int)data.B, (int)data.C, (int)data.D, (int)data.E, (int)data.F);

    void Reinitialize()
    {
        Awake();
        Start();
    }

    [System.Serializable]
    public class DiceData
    {
        public Sides A;
        public Sides B;
        public Sides C;
        public Sides D;
        public Sides E;
        public Sides F;
    }
    [System.Serializable]
    public class DiceColliders
    {
        public LaserReciever_Dice A;
        public LaserReciever_Dice B;
        public LaserReciever_Dice C;
        public LaserReciever_Dice D;
        public LaserReciever_Dice E;
        public LaserReciever_Dice F;
    }

    void OnDrawGizmosSelected()
    {
        DisplaySide(colliders.A, data.A, "A");
        DisplaySide(colliders.B, data.B, "B");
        DisplaySide(colliders.C, data.C, "C");
        DisplaySide(colliders.D, data.D, "D");
        DisplaySide(colliders.E, data.E, "E");
        DisplaySide(colliders.F, data.F, "F");
    }
    void DisplaySide(LaserReciever_Dice side, Sides data, string name)
    {
        if (side == null)
            return;

#if UNITY_EDITOR
        var style = new GUIStyle(UnityEditor.EditorStyles.boldLabel);
        style.normal.background = Texture2D.blackTexture;
        var text = $"{name} - {data.ToString().Replace("_", "")}";
        var pos = side.transform.position;

        style.fontSize = 25;

        style.normal.textColor = Color.black;
        UnityEditor.Handles.Label(pos, text, style);
        style.normal.textColor = Color.yellow;
        UnityEditor.Handles.Label(pos + Vector3.up * 0.003f, text, style);
#endif
    }
}
