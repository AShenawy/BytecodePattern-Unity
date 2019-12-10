using UnityEngine;
using System.Collections;

public class Subtraction : MonoBehaviour
{
    [SerializeField] private Calculator calculator;
    public void DoMath()
    {
        calculator.calculation = (CalculationType)0xB;
    }
}
