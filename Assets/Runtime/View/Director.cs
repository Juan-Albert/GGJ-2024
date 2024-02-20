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

    private Tempo _tempo;

    public async void Print(Note note, Rhythm.Result result)
    {
        PrintClownSprite();
        await PrintIdleAfterDelay(); 

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

    public void BeOnTime(Tempo tempo) => _tempo = tempo;

    private async Task PrintIdleAfterDelay()
    {
        var waitInMilliseconds = Mathf.FloorToInt(_tempo.ToSeconds(.3f) * 1000);
        await Task.Delay(waitInMilliseconds);
        clownRenderer.sprite = idleSprite;
    }
}