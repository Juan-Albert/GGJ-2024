using System.Linq;
using UnityEngine;

namespace Runtime.View
{
    public class VfxOnBeat : MonoBehaviour
    {
        private void OnEnable() => BeatSoundPlayer.onRhythmBeat += PlayVfx;
        private void OnDisable() => BeatSoundPlayer.onRhythmBeat -= PlayVfx;

        private void PlayVfx() => GetComponentsInChildren<ParticleSystem>().First().Play();
    }
}