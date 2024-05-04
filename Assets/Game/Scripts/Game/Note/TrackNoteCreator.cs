using Cysharp.Threading.Tasks;
using Game.Cfg.Game;
using UnityEngine;

public class TrackNoteCreator : NoteCreator
{
    private TrackNoteUnit _trackNoteUnit;
    public override async UniTask Create(NoteUnit noteUnit, Transform parent)
    {
        _trackNoteUnit = noteUnit as TrackNoteUnit;

        var timeMs = _trackNoteUnit.TimeMs;
        await UniTask.Delay(timeMs);

        var startPosition = _trackNoteUnit.StartPosition;
        var endPosition = _trackNoteUnit.EndPosition;
        var count = _trackNoteUnit.Count;
        var prefab = Resources.Load<GameObject>("Prefabs/TrackNote");

        for (var i = 0; i < count; i++)
        {
            var position = Vector3.Lerp(startPosition, endPosition, (float)i / count);
            var gob = Object.Instantiate(prefab, parent);
            gob.transform.localPosition = position;
            var note = gob.GetComponent<TrackNote>();
            note.NoteUnit = _trackNoteUnit;
            note.Show();
        }
    }
}
