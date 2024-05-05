using Cysharp.Threading.Tasks;
using Game.Cfg.Game;
using UnityEngine;

public class TrackNoteCreator : NoteCreator
{
    private TrackNoteUnit _trackNoteUnit;
    private static ResourcesGameObjectPool _trackNotePool = new ResourcesGameObjectPool("Prefabs/TrackNote");
    public override async UniTask Create(NoteUnit noteUnit, Transform parent)
    {
        _trackNoteUnit = noteUnit as TrackNoteUnit;

        var timeMs = _trackNoteUnit.TimeMs;
        await UniTask.Delay(timeMs);

        var startPosition = _trackNoteUnit.StartPosition;
        var endPosition = _trackNoteUnit.EndPosition;
        var count = _trackNoteUnit.Count;

        for (var i = 0; i < count; i++)
        {
            var position = Vector3.Lerp(startPosition, endPosition, (float)i / count);
            var gob = _trackNotePool.Get(parent);
            gob.transform.localPosition = position;
            var note = gob.GetComponent<TrackNote>();
            note.Pool = _trackNotePool;
            note.NoteUnit = _trackNoteUnit;
            note.Show();
        }
    }
}
