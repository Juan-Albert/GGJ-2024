﻿using System;
using Runtime.Domain;
using UnityEngine;

public class BeatSoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSelector audioSelector;
        
    private Sheet sheet;
    private void Awake() => sheet = CreateSheet();

    private void Update()
    {
        if (sheet.HasEnded)
            sheet = CreateSheet();
        
        audioSelector.Play(sheet.Play());
        sheet.PassTime(Time.deltaTime);
    }

    private static Sheet CreateSheet() => Sheet.OneBeatSheet;
}