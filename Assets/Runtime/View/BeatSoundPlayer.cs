using Runtime.Domain;
using UnityEngine;

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