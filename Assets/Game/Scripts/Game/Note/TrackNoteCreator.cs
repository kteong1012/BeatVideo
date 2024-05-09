using Cysharp.Threading.Tasks;

using UnityEngine;

public class TrackNoteCreator
{
    public static string TrackNotePrefabPath = "Prefabs/TrackNote";

    private NoteUnit _trackNoteUnit;
    public async UniTask Create(NoteUnit noteUnit, Transform parent, Vector2 _)
    {
        _trackNoteUnit = noteUnit;

        var timeMs = _trackNoteUnit.TimeMs;
        await UniTask.Delay(timeMs);

        var startPosition = _trackNoteUnit.StartPosition;
        var endPosition = _trackNoteUnit.EndPosition;
        var count = _trackNoteUnit.Count;

        for (var i = 0; i < count; i++)
        {
            var position = Vector3.Lerp(startPosition, endPosition, (float)i / count);
            var gob = PoolManager.Instance.Get(TrackNotePrefabPath, parent);
            gob.transform.localPosition = position;
            var note = gob.GetComponent<TrackNote>();
            note.NoteUnit = _trackNoteUnit;
            note.Show(_);
        }
    }
}
