using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.View
{
    public class PulseOnBeat : MonoBehaviour
    {
        private void OnEnable() => BeatSoundPlayer.onRhythmBeat += Pulse;
        private void OnDisable() => BeatSoundPlayer.onRhythmBeat -= Pulse;

        private void Pulse()
        {
            this.DOComplete();
            transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
        }
    }
}