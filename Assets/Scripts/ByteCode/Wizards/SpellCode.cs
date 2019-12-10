using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCode : MonoBehaviour
{
    public int[] spellCodes;

    [SerializeField]private VM virtualMachine;

    public void CastSpell()
    {
        virtualMachine.SortInstructions(spellCodes, spellCodes.Length); // Send spell codes to VM on button click
    }
}