using Cysharp.Threading.Tasks;
using Game.Cfg.Game;
using UnityEngine;

public class TrackNote : Note
{
    private static ResourcesGameObjectPool _clickEffectPool = new ResourcesGameObjectPool("Effects/Click");
    public TrackNoteUnit NoteUnit { get; set; }

    private bool _active;
    public override void Show()
    {
        _active = true;
        WaitToFadeOut().Forget();
    }

    protected override void OnClickNote()
    {
        OnClick();
    }

    protected override void OnEnterNote()
    {
        OnClick();
    }

    private async UniTask WaitToFadeOut()
    {
        var durationMs = NoteUnit.DurationMs;
        await UniTask.Delay(durationMs);

        if (_active)
        {
            Pool.Release(gameObject);
            _active = false;
        }
    }

    private void OnClick()
    {
        var effectGob = _clickEffectPool.Get(transform.parent);
        var effect = effectGob.GetComponent<NoteClickEffect>();
        effect.transform.position = transform.position;
        effect.Pool = _clickEffectPool;

        if (_active)
        {
            Pool.Release(gameObject);
            _active = false;
        }
    }
}
