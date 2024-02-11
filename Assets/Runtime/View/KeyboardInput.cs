using Runtime.Domain;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, MusicianInput
{
    public Note CaptureInput() 
        => Input.GetKeyDown(KeyCode.Space) ? "Sound" : "Silence";
}