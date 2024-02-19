using System.Linq;
using Runtime.Domain;
using UnityEngine;

//Poner el idle despues de animar en funcion del tempo
//El jugador tiene X intentos
//Cuando se falla una nota se pierde un intento
//Cuando se falla una nota se tiene un tiempo de invulnerabilidad
//Cuando se pierden todos los intentos se pierde la partida
//Hacer un evento de cuando suena un beat de la sheet de ritmo
//Reaccionar al evento con animaciones y efectos en la vista

public class BeatSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSelector audioSelector;
    private MusicianInput musicianInput;
    private MusicianOutput directorOutput;
    private MusicianOutput musicianOutput;
        
    private Sheet music;
    private Sheet rhythm;
    private Musician musician;
    private void Awake()
    {
        directorOutput = GetComponent<Director>();
        musicianOutput = GetComponent<Clown>();
        musicianInput = GetComponent<MusicianInput>();
        CreateConcert();
    }

    private void Update()
    {
        if (music.HasEnded)
            CreateConcert();
        
        PlayRhythm();
        PlayMusic();
    }

    void PlayRhythm()
    {
        rhythm.PassTime(Time.deltaTime);
        music.PassTime(Time.deltaTime);
        audioSelector.Play(rhythm.Read());
    }

    private void PlayMusic()
    {
        ShowDirector();
        CheckMusicianPlay();
    }

    private void ShowDirector()
    {
        var noteInSheet = music.Read();
        if (!noteInSheet.Equals(Note.Silence))
        {
            directorOutput.Print(noteInSheet, Rhythm.Result.Perfect);
        }
    }

    private void CheckMusicianPlay()
    {
        var input = musicianInput.CaptureInput();
        if (!input.Equals(Note.Silence))
        {
            var played = new Note(input);
            var result = musician.Play(played);
            musicianOutput.Print(played, result);
        }
    }


    #region Factories

    private void CreateConcert()
    {
        music = CreateSheet();
        rhythm = CreateSheet();
        musician = CreateInstrument();
    }
    
    private Musician CreateInstrument() => new(music);

    private static Sheet CreateSheet() => OneNoteSheet();

    private static Sheet OneNoteSheet()
    {
        return new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []
        {
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Handstand),
        });
    }

    private static Sheet TwoNotesSheet()
    {
        return new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []
        {
            new Beat(1, Note.Ball),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Ball),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Ball),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Ball),
            new Beat(1, Note.Handstand),
        });
    }
    
    private static Sheet FourNotesSheet()
    {
        return new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []
        {
            new Beat(1, Note.Ball),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Juggle),
            new Beat(1, Note.Trumpet),
            new Beat(1, Note.Ball),
            new Beat(1, Note.Handstand),
            new Beat(1, Note.Juggle),
            new Beat(1, Note.Trumpet)
        });
    }

    #endregion
}