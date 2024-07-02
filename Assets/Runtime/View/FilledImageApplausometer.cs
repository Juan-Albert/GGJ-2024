using System;
using Runtime.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.View
{
    public class FilledImageApplausometer : MonoBehaviour
    {
        public Image filledImage;
        public Applausometer applausometer;

        private void Awake() => applausometer = new Applausometer();

        private void Update() => filledImage.fillAmount = applausometer.ApplauseMeter / Applausometer.MaxApplauseMeter;
    }
}