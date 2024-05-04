using Cysharp.Threading.Tasks;
using Game.Cfg.Game;
using UnityEngine;

public abstract class NoteCreator
{
    public abstract UniTask Create(NoteUnit noteUnit, Transform parent);
}
