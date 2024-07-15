using System;
using Runtime.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.View
{
    public class ApplausometerView : MonoBehaviour
    {
        public Image filledImage;
        public TextMeshProUGUI comboText;

        public Applausometer applausometer;

        private void Awake() => applausometer = new Applausometer();

        private void Update()
        {
            comboText.text = $"x{applausometer.ApplauseCombo.Counter}";
            filledImage.fillAmount = applausometer.ApplauseMeter / Applausometer.MaxApplauseMeter;
        }
    }
}