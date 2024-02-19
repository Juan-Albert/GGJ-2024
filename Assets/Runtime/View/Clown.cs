using System;
using System.Threading.Tasks;
using Runtime.Domain;
using UnityEngine;

public class Clown : MonoBehaviour, MusicianOutput
{
    [SerializeField] private SpriteRenderer clownRenderer;
    
    [SerializeField] private Sprite ballSprite;
    [SerializeField] private Sprite handstandSprite;
    [SerializeField] private Sprite juggleSprite;
    [SerializeField] private Sprite trumpetSprite;
    [SerializeField] private Sprite idleSprite;
    [SerializeField] private Sprite failSprite;

    [SerializeField] private ParticleSystem perfectVfx;
    [SerializeField] private ParticleSystem greatVfx;
    [SerializeField] private ParticleSystem goodVfx;
    [SerializeField] private ParticleSystem outVfx;
    
    
    public void Print(Note note, Rhythm.Result result)
    {
        PrintNote(note, result);
        PrintResult(result);
    }

    private async void PrintNote(Note note, Rhythm.Result result)
    {
        if (result.Equals(Rhythm.Result.Out))
        {
            clownRenderer.sprite = failSprite;
        }
        else
        {
            PrintClownSprite();
        }

        await PrintIdleAfterDelay();
        return;

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

    private void PrintResult(Rhythm.Result result)
    {
        switch (result)
        {
            case Rhythm.Result.Out:
                outVfx.Play();
                break;
            case Rhythm.Result.Good:
                goodVfx.Play();
                break;
            case Rhythm.Result.Great:
                greatVfx.Play();
                break;
            case Rhythm.Result.Perfect:
                perfectVfx.Play();
                break;
            default:
                throw new NotSupportedException("No exite este resultado para el payaso");
        }
    }
}