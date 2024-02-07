using System;
using Runtime.Domain;
using UnityEngine;

public class LogOutput : MonoBehaviour, RhythmOutput
{
    public void Print(OnTime.Result result)
    {
        switch (result)
        {
            case OnTime.Result.Out:
                Debug.Log("LAMENTABLE");
                break;
            case OnTime.Result.Good:
                Debug.Log("BIEEEEN");
                break;
            case OnTime.Result.Great:
                Debug.Log("INCREIBLEE");
                break;
            case OnTime.Result.Perfect:
                Debug.Log("OH MY BOY IS AMAZIIIIING");
                break;
            default:
                throw new NotSupportedException("No existe ese resultado");
        }
    }
}