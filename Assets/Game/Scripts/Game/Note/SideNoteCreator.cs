using Cysharp.Threading.Tasks;

using UnityEngine;


public class SideNoteCreator
{
    public static string SideNotePrefabPath ="Prefabs/SideNote";
    private NoteUnit _sideNoteUnit;

    public async UniTask Create(NoteUnit noteUnit, Transform parent, Vector2 canvasSize)
    {
        _sideNoteUnit = noteUnit;
        var speed = _sideNoteUnit.Speed;
        var direction = _sideNoteUnit.Direction;
        var halfHeight = canvasSize.y / 2;
        var halfWidth = canvasSize.x / 2;
        var length = direction switch
        {
            NoteDirection.Left => halfWidth - _sideNoteUnit.Position.x,
            NoteDirection.Right => _sideNoteUnit.Position.x + halfWidth,
            NoteDirection.Down => halfHeight - _sideNoteUnit.Position.y,
            NoteDirection.Up => _sideNoteUnit.Position.y + halfHeight,
            _ => throw new System.ArgumentOutOfRangeException()
        };
        var flyTimeMs = length / speed * 1000;
        var showTimeMs = _sideNoteUnit.TimeMs - flyTimeMs;
        await UniTask.Delay((int)showTimeMs);
        var gob = PoolManager.Instance.Get(SideNotePrefabPath,parent);
        var startPostion = direction switch
        {
            NoteDirection.Left => new Vector3(halfWidth, _sideNoteUnit.Position.y, 0),
            NoteDirection.Right => new Vector3(-halfWidth, _sideNoteUnit.Position.y, 0),
            NoteDirection.Down => new Vector3(_sideNoteUnit.Position.x, halfHeight, 0),
            NoteDirection.Up => new Vector3(_sideNoteUnit.Position.x, -halfHeight, 0),
            _ => throw new System.ArgumentOutOfRangeException()
        };
        gob.transform.localPosition = startPostion;
        var note = gob.GetComponent<SideNote>();
        note.NoteUnit = _sideNoteUnit;
        note.Show(canvasSize);
    }
}
