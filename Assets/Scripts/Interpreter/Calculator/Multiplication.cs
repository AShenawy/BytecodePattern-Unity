using UnityEngine;
using System.Collections;

public class Multiplication : MonoBehaviour
{
    [SerializeField] private Calculator calculator;
    public void DoMath()
    {
        calculator.calculation = (CalculationType)0xC;
    }
}
