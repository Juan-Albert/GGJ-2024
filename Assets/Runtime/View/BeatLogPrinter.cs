using System.Collections;
using System.Collections.Generic;
using Runtime.Domain;
using UnityEngine;

public class BeatLogPrinter : MonoBehaviour
{
    private Sheet sheet;

    private void Awake() => sheet = CreateSheet();

    private void Update()
    {
        if (sheet.HasEnded)
            sheet = CreateSheet();

        Debug.Log(sheet.PlayCurrentBeat());
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