using Runtime.Domain;
using UnityEngine;

public class KeyboardInput : MonoBehaviour, MusicianInput
{
    public Note CaptureInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Note.Trumpet;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Note.Juggle;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Note.Handstand;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Note.Ball;
        }
        
        return Note.Silence;
    }
}