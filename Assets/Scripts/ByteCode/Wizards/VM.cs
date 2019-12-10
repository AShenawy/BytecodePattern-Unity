using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VM : MonoBehaviour
{
    public GameObject[] player;
    public AudioClip[] audioClips;
    public GameObject[] particleEffects;

    [Header("Instructions")]
    [SerializeField]private Instructions instruction;
    
    // A stack to hold all the values sent by the Literal instruction
    private Stack valueStack = new Stack();
    private int value;  // temp variable to hold the values to be stored on the stack

    // Main body of the virtual machine which goes through the instructions in bytecode and executes them
    #region VM Instruction Pool
    public void SortInstructions(int[] bytecode, int size)  // <<< DO I NEED THIS 'size' ARGUMENT?
    {
        for (int i = 0; i < bytecode.Length; i++) 
        {
            instruction = (Instructions)bytecode[i];    // for every value in the bytecode array, translate it into instruction for execution

            switch (instruction)
            {
                case Instructions.INST_GET_HEALTH:
                    int wizardHP = PopOffStack();       // which wizard to get health of
                    PushOnStack(GetHealth(wizardHP));   // get the health and push it on stack
                    break;

                case Instructions.INST_SET_HEALTH:
                    int hPAmount = PopOffStack();   // how much to set health to
                    int hPWizard = PopOffStack();   // which wizard to set health of
                    SetHealth(hPWizard, hPAmount);
                    break;

                case Instructions.INST_GET_WISDOM:  // Same as health for wisdom & agility
                    int wizardWP = PopOffStack();
                    PushOnStack(GetWisdom(wizardWP));
                    break;

                case Instructions.INST_SET_WISDOM:
                    int wPAmount = PopOffStack();
                    int wPWizard = PopOffStack();
                    SetWisdom(wPWizard, wPAmount);
                    break;

                case Instructions.INST_GET_AGILITY:
                    int wizardAP = PopOffStack();
                    PushOnStack(GetAgility(wizardAP));
                    break;

                case Instructions.INST_SET_AGILITY:
                    int aPamount = PopOffStack();
                    int aPwizard = PopOffStack();
                    SetAgility(aPwizard, aPamount);
                    break;

                case Instructions.INST_ADD:
                    int bAdd = PopOffStack();
                    int aAdd = PopOffStack();
                    PushOnStack(aAdd + bAdd);       // The addition result is stored on stack to be used with other instructions
                    print($"Added {bAdd} to {aAdd}");
                    break;

                case Instructions.INST_SUBTRACT:
                    int bSub = PopOffStack();
                    int aSub = PopOffStack();
                    PushOnStack(aSub - bSub);       // Same as addition for subtraction, multiplication and division
                    print($"Subtracted {bSub} from {aSub}");
                    break;

                case Instructions.INST_MULTIPLY:
                    int bMul = PopOffStack();
                    int aMul = PopOffStack();
                    PushOnStack(aMul * bMul);
                    print($"Multiplied {aMul} by {bMul}");
                    break;

                case Instructions.INST_DIVIDE:
                    int bDiv = PopOffStack();
                    int aDiv = PopOffStack();
                    PushOnStack(aDiv / bDiv);
                    print($"Divided {aDiv} by {bDiv}");
                    break;

                case Instructions.INST_PLAY_SOUND:
                    int soundID = PopOffStack();
                    PlaySound(soundID);
                    break;

                case Instructions.INST_SPAWN_PARTICLES:
                    int particleType = PopOffStack();
                    int targetWizard = PopOffStack();
                    SpawnParticles(targetWizard, particleType);
                    break;

                case Instructions.INST_LITERAL:
                    value = bytecode[++i];
                    PushOnStack(value);
                    print($"Added {value} on the stack");
                    break;

                default:
                    break;
            }
        }
    }
    #endregion

    // Methods for pushing onto/popping off values stack
    #region Stack Methods
    void PushOnStack(int value)
    {
        valueStack.Push(value);
    }

    int PopOffStack()
    {
        if (valueStack.Count > 0)
            return (int)valueStack.Pop();
        else
        {
            print("Spell stack empty.");
            return 0;
        }
    }
    #endregion

    // Get/Set Health
    #region Health Mehtods
    int GetHealth(int wizard)
    {
        int health = player[wizard].GetComponent<Wizard>().health;
        print($"{player[wizard].name}'s health is now {health}");
        return health;
    }

    void SetHealth(int wizard, int amount)
    {
        player[wizard].GetComponent<Wizard>().health = amount;
        print($"{player[wizard].name}'s health has become {amount}!");
    }
    #endregion

    // Get/Set Wisdom
    #region Wisdom Methods
    int GetWisdom(int wizard)
    {
        int wisdom = player[wizard].GetComponent<Wizard>().wisdom;
        print($"{player[wizard].name}'s wisdom is now {wisdom}");
        return wisdom;
    }

    void SetWisdom(int wizard, int amount)
    {
        player[wizard].GetComponent<Wizard>().wisdom = amount;
        print($"{player[wizard].name}'s wisdom has become {amount}!");
    }
    #endregion

    // Get/Set Agility
    #region Agillity Methods
    int GetAgility(int wizard)
    {
        int agility = player[wizard].GetComponent<Wizard>().agility;
        print($"{player[wizard].name}'s agility is now {agility}");
        return agility;
    }

    void SetAgility(int wizard, int amount)
    {
        player[wizard].GetComponent<Wizard>().agility = amount;
        print($"{player[wizard].name}'s agility has become {amount}!");
    }
    #endregion

    // Sound, Particles, etc
    #region Effects Methods
    void PlaySound(int soundId)
    {
        if (audioClips.Length > 0)
        {
            AudioSource.PlayClipAtPoint(audioClips[soundId], Vector3.zero); // Play sound at world center. location doesn't affect volume
            print($"{audioClips[soundId]} sound playing");
        }
    }

    void SpawnParticles(int wizard, int particleType)
    {
        if (particleEffects.Length > 0)
        {
            Instantiate(particleEffects[particleType], player[wizard].transform);   // Create the particle effect at which wizard
            print($"{particleEffects[particleType].name} spawned at {player[wizard].name}!");
        }
    }
    #endregion
}

// A list of the methods which will be sent by bytecode and provide actions
public enum Instructions
{
    INST_GET_HEALTH = 100,
    INST_SET_HEALTH = 101,
    INST_GET_WISDOM = 200,
    INST_SET_WISDOM = 201,
    INST_GET_AGILITY = 300,
    INST_SET_AGILITY = 301,
    // An instruction to set values required for methods' arguments
    INST_LITERAL = 10,
    INST_ADD = 20,
    INST_SUBTRACT = 21,
    INST_MULTIPLY = 22,
    INST_DIVIDE = 23,
    INST_PLAY_SOUND = 30,
    INST_SPAWN_PARTICLES = 31
}
