using System;
using UnityEngine;

public enum NoteDirection
{
    Up,
    Down,
    Left,
    Right
}

public enum NoteType
{
    SideNote,
    TrackNote,
}


[Serializable]
public class NoteUnit
{
    public NoteType NoteType;

    public int TimeMs;
    public int Score;
    public string EffectPath;
    public string SoundPath;

    public Vector2 Position;
    public NoteDirection Direction;
    public int Speed;
    public int DurationMs;

    public Vector2 StartPosition;
    public Vector2 EndPosition;
    public int Count;

}