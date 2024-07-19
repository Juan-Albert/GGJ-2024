using System.Linq;
using Runtime.Domain;
using UnityEngine;

//Dar mas feedback de que se ha fallado
//Crear ciclo de juego
    //Menu de inicio
    //Pantalla final: Score???

//Sonido 
    //distintos sonidos a los movimientos del director?
    //Cambiar el pitch cada vez que se hace play al sonido tanto del director como del rhythm

//Pequeño tutorial en canvas
//Crear todos los ritmos


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

        private int sheetsPlayed;
        private Song directorSong;
        private Song musicianSong;
        private Musician musician;
        private ApplausometerView applausometerview;

        private void Awake()
        {
            directorOutput = GetComponent<Director>();
            musicianOutput = GetComponent<Clown>();
            musicianInput = GetComponent<MusicianInput>();
            applausometerview = FindObjectsOfType<ApplausometerView>().Single();
            sheetsPlayed = 0;
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

            if (applausometerview.applausometer.ApplauseMeter <= 0f)
                GameOver();
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
                applausometerview.applausometer.ReactTo(result);
            }
            else if(musician.HasFailedLastBeat())
            {
                musician.FailLastBeat();
                musicianOutput.Print(Note.Silence, Rhythm.Result.Out);
                applausometerview.applausometer.ReactTo(Rhythm.Result.Out);
            }
        }

        private void GameOver()
        {
            //TODO enseñar la pantalla de gameover
            applausometerview.applausometer.Reset();
        }

        #region Factories

        private void CreateConcert()
        {
            directorSong = Composer.ComposeBasedOn(sheetsPlayed);
            musicianSong = directorSong.AsCopy();
            musician = PrepareMusician();
            musicianOutput.BeOnTime(directorSong.Tempo);
            directorOutput.BeOnTime(directorSong.Tempo);
            sheetsPlayed++;
        }

        private Musician PrepareMusician() => new(musicianSong.Music);
        #endregion
    }
}