using Runtime.Domain;
using UnityEngine;


//El resultado del Ritmo tiene que estar basado en el tempo de la partitura
//Crear las notas del juego
//Crear los inputs disponibles
//Animar el payaso en funcion del output
//La partitura tiene un resultado en funcion de lo bien que se haya tocado
//  Si no se toca una nota es un fallo en el resultado de la partitura
//  los fallos se resuelven al final de la partitura?
//Hacer un evento de cuando suena un beat de la sheet de ritmo
//Reaccionar al evento con animaciones y efectos en la vista

public class BeatSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSelector audioSelector;
    private MusicianInput _musicianInput;
    private MusicianOutput _musicianOutput;
        
    private Sheet sheet;
    private Sheet rhythm;
    private Musician musician;
    private void Awake()
    {
        _musicianInput = GetComponent<MusicianInput>();
        _musicianOutput = GetComponent<MusicianOutput>();
        CreateConcert();
    }

    private void Update()
    {
        if (sheet.HasEnded)
            CreateConcert();
        
        PlayRhythm();
        CheckMusicianPlay();
    }

    void PlayRhythm()
    {
        rhythm.PassTime(Time.deltaTime);
        sheet.PassTime(Time.deltaTime);
        audioSelector.Play(rhythm.Read());
    }

    private void CheckMusicianPlay()
    {
        var input = _musicianInput.CaptureInput();
        if (!input.Equals(Note.Silence))
        {
            var result = musician.Play(new Note(input));
            _musicianOutput.Print(result);
        }
    }


    #region Builders

    private void CreateConcert()
    {
        sheet = CreateSheet();
        rhythm = CreateSheet();
        musician = CreateInstrument();
    }
    
    private Musician CreateInstrument() => new(sheet);

    private static Sheet CreateSheet()
    {
        return new Sheet(Tempo.OneBeatPerSecond, new ForwardTime(), new []
        {
            new Beat(1, "Sound"),
            new Beat(1, "Sound"),
            new Beat(1, "Sound"),
            new Beat(1, "Sound"),
            new Beat(1, "Sound"),
            new Beat(1, "Sound"),
            new Beat(1, "Sound"),
            new Beat(1, "Sound")
        });
    }

    #endregion
}