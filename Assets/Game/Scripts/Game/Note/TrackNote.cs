using Cysharp.Threading.Tasks;
using Game.Cfg.Game;
using UnityEngine;

public class TrackNote : Note
{
    public TrackNoteUnit NoteUnit { get; set; }
    public override void Show()
    {

    }

    protected override void OnClickNote()
    {
        OnClick();
    }

    protected override void OnEnterNote()
    {
        OnClick();
    }

    private void OnClick()
    {
        var clickPrefab = Resources.Load<GameObject>("Effects/Click");
        var gob = Instantiate(clickPrefab, transform);
        gob.transform.SetParent(transform.parent);
        Destroy(gameObject);
    }
}
