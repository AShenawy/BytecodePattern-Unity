using UnityEngine;
using System.Collections;

public class Division : MonoBehaviour
{
    [SerializeField] private Calculator calculator;
    public void DoMath()
    {
        calculator.calculation = (CalculationType)0xD;
    }
}
