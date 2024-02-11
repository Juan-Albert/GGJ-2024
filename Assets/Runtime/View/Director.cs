using System;
using System.Threading.Tasks;
using Runtime.Domain;
using UnityEngine;

public class Director : MonoBehaviour, MusicianOutput
{
    [SerializeField] private SpriteRenderer clownRenderer;
    
    [SerializeField] private Sprite ballSprite;
    [SerializeField] private Sprite handstandSprite;
    [SerializeField] private Sprite juggleSprite;
    [SerializeField] private Sprite trumpetSprite;
    [SerializeField] private Sprite idleSprite;
    
    
    public void Print(Note note, Rhythm.Result result)
    {
        PrintClownSprite();
        PrintIdleAfterDelay(); //Si le hago await tambien tengo que hacerselo al padre y asi sucesivamente

        void PrintClownSprite()
        {
            if (note.Equals(Note.Silence))
                clownRenderer.sprite = idleSprite;
            else if (note.Equals(Note.Ball))
                clownRenderer.sprite = ballSprite;
            else if (note.Equals(Note.Handstand))
                clownRenderer.sprite = handstandSprite;
            else if (note.Equals(Note.Juggle))
                clownRenderer.sprite = juggleSprite;
            else if (note.Equals(Note.Trumpet))
                clownRenderer.sprite = trumpetSprite;
            else
                throw new NotSupportedException("No existe el sprite de esa nota");
        }
    }

    private async Task PrintIdleAfterDelay()
    {
        await Task.Delay(300);
        clownRenderer.sprite = idleSprite;
    }
}