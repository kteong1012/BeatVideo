using Cysharp.Threading.Tasks;
using Game.Cfg.Game;
using UnityEngine;

public class TrackNote : Note
{
    private static ResourcesGameObjectPool _clickEffectPool = new ResourcesGameObjectPool("Effects/Click");
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
        var effectGob = _clickEffectPool.Get(transform.parent);
        var effect = effectGob.GetComponent<NoteClickEffect>();
        effect.transform.position = transform.position;
        effect.Pool = _clickEffectPool;
        Pool.Release(gameObject);
    }
}
