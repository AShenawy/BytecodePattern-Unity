using UnityEngine;
using System.Collections;

public class AdditionExpression : Expression
{
    [HideInInspector]public double leftOperand, rightOperand;

    // Use this for initialization
    void Start()
    {
        Evaluate();
    }

    public virtual double Evaluate()
    {
        double left = leftOperand;
        double right = rightOperand;

        return left + right;
    }
}
