using Runtime.Domain;

internal interface MusicianOutput
{
    void Print(Note note, Rhythm.Result result);
    void BeOnTime(Tempo tempo);
}