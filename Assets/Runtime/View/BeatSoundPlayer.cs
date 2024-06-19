using Runtime.Domain;
using UnityEngine;
using UnityEngine.Playables;

//Crear ciclo de juego
    //Primero el directo toca la secuencia y luego el musico
//Aplausometro
    //Cuando se falla una nota baja el aplausometro
    //Cuando se haciertan varias en racha el aplausometro sube
    //Cuando se falla una nota se tiene un tiempo de invulnerabilidad
    //Cuando la barra de aplausometro se acaba se pierde la partida
//Crear todos los ritmos
//Pequeño tutorial en canvas
//Pantalla final: Score???
//Sonido 
    //distintos sonidos a los movimientos del director?
    //Cambiar el pitch cada vez que se hace play al sonido tanto del director como del rhythm



namespace Runtime.View
{
    public class BeatSoundPlayer : MonoBehaviour
    {
        public delegate void OnRhythmBeat();
        public static event OnRhythmBeat onRhythmBeat;
    
        [SerializeField] private AudioSelector audioSelector;
        private MusicianInput musicianInput;
        private MusicianOutput directorOutput;
        private MusicianOutput musicianOutput;

        private Song directorSong;
        private Song musicianSong;
        private Musician musician;
        private void Awake()
        {
            directorOutput = GetComponent<Director>();
            musicianOutput = GetComponent<Clown>();
            musicianInput = GetComponent<MusicianInput>();
            CreateConcert();
        }

        private void Update()
        {
            if (directorSong.HasEnded && musicianSong.HasEnded)
                CreateConcert();

            if (directorSong.HasEnded)
                PlayMusician();
            else
                PlayDirector();
        }

        private void PlayDirector()
        {
            directorSong.PassTime(Time.deltaTime);
            PlayBeatOf(directorSong);
            ShowDirector();
        }

        private void PlayMusician()
        {
            musicianSong.PassTime(Time.deltaTime);
            PlayBeatOf(musicianSong);
            CheckMusicianPlay();
        }

        void PlayBeatOf(Song song)
        {
            var beatSound = song.PlayRhythm();
            audioSelector.Play(beatSound);
            
            if (beatSound != Note.Silence.Sound)
                onRhythmBeat?.Invoke();
        }
        
        private void ShowDirector()
        {
            var noteInSheet = directorSong.PlayMusic();
            if (!noteInSheet.Equals(Note.Silence))
            {
                directorOutput.Print(noteInSheet, Rhythm.Result.Perfect);
            }
        }
        
        private void CheckMusicianPlay()
        {
            var input = musicianInput.CaptureInput();
            if (!input.Equals(Note.Silence))
            {
                var played = new Note(input);
                var result = musician.Play(played);
                musicianOutput.Print(played, result);
            }
            else if(musician.HasFailedLastBeat())
            {
                musician.Play(Note.Wrong);
                musicianOutput.Print(Note.Silence, Rhythm.Result.Out);
            }
        }

        #region Factories

        private void CreateConcert()
        {
            directorSong = Composer.Compose();
            musicianSong = directorSong.AsCopy();
            musician = PrepareMusician();
            musicianOutput.BeOnTime(Tempo.OneBeatPerSecond);
            directorOutput.BeOnTime(Tempo.OneBeatPerSecond);
        }

        private Musician PrepareMusician() => new(musicianSong.Music);
        #endregion
    }
}