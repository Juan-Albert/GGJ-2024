using System;
using Runtime.Domain;
using UnityEngine;

public class BeatSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSelector audioSelector;
        
    private Sheet sheet;
    private void Awake() => sheet = CreateSheet();

    private void FixedUpdate()
    {
        if (sheet.HasEnded)
            sheet = CreateSheet();

        audioSelector.Play(sheet.PlayCurrentBeat());
        sheet.NextFrame();
    }

    private static Sheet CreateSheet() 
        => new(new[]
        {
            Beat.Sound,
            Beat.Sound,
            Beat.Sound,
            Beat.Sound
        });
}