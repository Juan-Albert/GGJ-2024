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

    }

    private static Sheet CreateSheet() => Sheet.OneBeatSheet;
}