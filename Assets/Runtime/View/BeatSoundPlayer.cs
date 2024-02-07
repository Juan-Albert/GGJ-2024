using Runtime.Domain;
using UnityEngine;

//Si ya se tocado una nota on time con un beat no se puede volver a tocar
//Si no se toca una nota es un fallo
//los fallos se resuelven al final de la partitura?
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
    private void Awake()
    {
        rhythmInput = GetComponent<RhythmInput>();
        rhythmOutput = GetComponent<RhythmOutput>();
        sheet = CreateSheet();
    }

    private void Update()
    {
        if (sheet.HasEnded)
            sheet = CreateSheet();
        
        audioSelector.Play(sheet.Play());
        sheet.PassTime(Time.deltaTime);
        var input = rhythmInput.CaptureInput();
        if (!input.Equals(Note.Silence))
        {
            var result = sheet.IsOnTime(new Note(input));
            rhythmOutput.Print(result);
        }
    }

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