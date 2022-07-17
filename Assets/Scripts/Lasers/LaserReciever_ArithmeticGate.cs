using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArithmeticOperation { SUM, SUB, MUL, DIV }
public class LaserReciever_ArithmeticGate : LaserReciever_Gate
{
    public int Number;
    [SerializeField] ArithmeticOperation Operation;
    [SerializeField] bool clampAbove0;
    [SerializeField] UI_BeamValueV2 ui;

    private void Awake()
    {
        if (ui == null)
            return;

        ui.Initialize(Camera.main, ui.transform.position);
        string operation = Operation switch
        {
            ArithmeticOperation.SUM => "+",
            ArithmeticOperation.SUB => "-",
            ArithmeticOperation.MUL => "×",
            ArithmeticOperation.DIV => "÷",
        };
        ui.Value.text = $"{operation}{Number}";
        ui.SetState(true);
    }

    public override void Interact(LaserBeam beam, ref Ray ray, RaycastHit info, bool isPreview, out bool continueBeam, AddNode addNode)
    {
        beam.TotalStrength = Compute(beam.TotalStrength, Number, Operation);

        base.Interact(beam, ref ray, info, isPreview, out continueBeam, addNode);
    }

    int Compute(int state, int param, ArithmeticOperation oper)
    {
        if (oper == ArithmeticOperation.DIV && param == 0)
            param = 1;

        var value = oper switch
        {
            ArithmeticOperation.SUM => state + param,
            ArithmeticOperation.SUB => state - param,
            ArithmeticOperation.MUL => state * param,
            ArithmeticOperation.DIV => state / param,
        };

        return clampAbove0 ? Mathf.Clamp(value, 0, int.MaxValue) : value;
    }
}
