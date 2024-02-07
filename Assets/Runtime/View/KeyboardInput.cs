using Runtime.Domain;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, RhythmInput
{
    public Note CaptureInput() 
        => Input.GetKeyDown(KeyCode.Space) ? "Sound" : "Silence";
}