using UnityEngine;
using System.Collections;

public class Addition : MonoBehaviour
{
    [SerializeField] private Calculator calculator;
    public void DoMath()
    {
        calculator.calculation = (CalculationType)0xA;
    }
}
