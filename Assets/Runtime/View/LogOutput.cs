using System;
using Runtime.Domain;
using UnityEngine;

public class LogOutput : MonoBehaviour, RhythmOutput
{
    public void Print(Rhythm.Result result)
    {
        switch (result)
        {
            case Rhythm.Result.Out:
                Debug.Log("LAMENTABLE");
                break;
            case Rhythm.Result.Good:
                Debug.Log("BIEEEEN");
                break;
            case Rhythm.Result.Great:
                Debug.Log("INCREIBLEE");
                break;
            case Rhythm.Result.Perfect:
                Debug.Log("OH MY BOY IS AMAZIIIIING");
                break;
            default:
                throw new NotSupportedException("No existe ese resultado");
        }
    }
}