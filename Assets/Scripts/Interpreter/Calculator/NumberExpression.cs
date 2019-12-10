using UnityEngine;
using System.Collections;

public class NumberExpression : Expression
{
    [SerializeField]private double value;

    public virtual double Evaluate()
    {
        return value;
    }

}
