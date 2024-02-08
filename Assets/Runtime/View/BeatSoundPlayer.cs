using Runtime.Domain;
using UnityEngine;

//La partitura tiene un resultado en funcion de lo bien que se haya tocado
//  Si no se toca una nota es un fallo en el resultado de la partitura
//  los fallos se resuelven al final de la partitura?
//tener una sheet para el ritmo y otra para lo que hay que tocar
//Crear las notas del juego
//Crear los inputs disponibles
//Animar el payaso en funcion del output
//Hacer un evento de cuando suena un beat de la sheet de ritmo
//Reaccionar al evento con animaciones y efectos en la vista

public class BeatSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSelector audioSelector;
    private RhythmInput rhythmInput;
    private RhythmOutput rhythmOutput;
        
    private Sheet sheet;
    private Musician _musician;
    private void Awake()
    {
        rhythmInput = GetComponent<RhythmInput>();
        rhythmOutput = GetComponent<RhythmOutput>();
        CreateConcert();
    }

    private void Update()
    {
        if (sheet.HasEnded)
            CreateConcert();
        
        audioSelector.Play(sheet.Play());
        sheet.PassTime(Time.deltaTime);
        var input = rhythmInput.CaptureInput();
        if (!input.Equals(Note.Silence))
        {
            var result = _musician.Play(new Note(input));
            rhythmOutput.Print(result);
        }
    }

    private void CreateConcert()
    {
        sheet = CreateSheet();
        _musician = CreateInstrument();
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
}