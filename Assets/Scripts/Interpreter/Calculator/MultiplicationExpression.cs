using UnityEngine;
using System.Collections;

public class MultiplicationExpression : Expression
{
    [HideInInspector] public double leftOperand, rightOperand;

    public virtual double Evaluate()
    {
        double left = leftOperand;
        double right = rightOperand;

        return left * right;
    }
}
