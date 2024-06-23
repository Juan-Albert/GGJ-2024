using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.View
{
    public class AudioSelector : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        public void Play(string soundId)
        {
            if(soundId != "Silence")
                PlayWithRandomPitch();
        }

        private void PlayWithRandomPitch()
        {
            audioSource.pitch = Mathf.Lerp(.95f, 1.05f, Random.value);
            audioSource.Play();
        }
    }
}