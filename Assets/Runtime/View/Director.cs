using System;
using System.Threading.Tasks;
using Runtime.Domain;
using UnityEngine;

public class Director : MonoBehaviour, MusicianOutput
{
    [SerializeField] private SpriteRenderer clownRenderer;
    [SerializeField] private SpriteRenderer arrowRenderer;

    [Header("Clown poses")]
    [SerializeField] private Sprite ballSprite;
    [SerializeField] private Sprite handstandSprite;
    [SerializeField] private Sprite juggleSprite;
    [SerializeField] private Sprite trumpetSprite;
    [SerializeField] private Sprite idleSprite;
    [Header("Key inputs")]
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite leftSprite;
    [SerializeField] private Sprite rightSprite;

    private Tempo _tempo;

    public async void Print(Note note, Rhythm.Result result)
    {
        PrintClownSprite();
        await PrintIdleAfterDelay(); 

        void PrintClownSprite()
        {
            if (note.Equals(Note.Silence))
            {
                clownRenderer.sprite = idleSprite;
                arrowRenderer.sprite = null;
            }
            else if (note.Equals(Note.Ball))
            {
                clownRenderer.sprite = ballSprite;
                arrowRenderer.sprite = upSprite;
            }
            else if (note.Equals(Note.Handstand))
            {
                clownRenderer.sprite = handstandSprite;
                arrowRenderer.sprite = downSprite;
            }
            else if (note.Equals(Note.Juggle))
            {
                clownRenderer.sprite = juggleSprite;
                arrowRenderer.sprite = rightSprite;
            }
            else if (note.Equals(Note.Trumpet))
            {
                clownRenderer.sprite = trumpetSprite;
                arrowRenderer.sprite = leftSprite;
            }
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