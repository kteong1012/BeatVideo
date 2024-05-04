using Cysharp.Threading.Tasks;
using Game.Cfg.Game;
using UnityEngine;
public class SideNoteCreator : NoteCreator
{
    private SideNoteUnit _sideNoteUnit;
    public override async UniTask Create(NoteUnit noteUnit, Transform parent)
    {
        _sideNoteUnit = (SideNoteUnit)noteUnit;
        var speed = _sideNoteUnit.Speed;
        var direction = _sideNoteUnit.Direction;
        var length = direction switch
        {
            SideNoteDirection.Left => 360f - _sideNoteUnit.Position.x,
            SideNoteDirection.Right => _sideNoteUnit.Position.x + 360f,
            _ => 0f
        };
        var flyTimeMs = length / speed * 1000;
        var showTimeMs = _sideNoteUnit.TimeMs - flyTimeMs;
        await UniTask.Delay((int)showTimeMs);
        var prefab = Resources.Load<GameObject>("Prefabs/SideNote");
        var gob = Object.Instantiate(prefab, parent);
        var startPostion = direction switch
        {
            SideNoteDirection.Left => new Vector3(420, _sideNoteUnit.Position.y, 0),
            SideNoteDirection.Right => new Vector3(-420, _sideNoteUnit.Position.y, 0),
            _ => Vector3.zero
        };
        gob.transform.localPosition = startPostion;
        var note = gob.GetComponent<SideNote>();
        note.NoteUnit = _sideNoteUnit;
        note.Show();
    }
}
