using Cysharp.Threading.Tasks;

using UnityEngine;

public class TrackNote : Note
{
    public NoteUnit NoteUnit { get; set; }

    private bool _active;
    public override void Show(Vector2 _)
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
            PoolManager.Instance.Release(TrackNoteCreator.TrackNotePrefabPath, gameObject);
            _active = false;
        }
    }

    private void OnClick()
    {
        var effectGob = PoolManager.Instance.Get(NoteUnit.EffectPath, transform.parent);
        var effect = effectGob.GetComponent<NoteClickEffect>();
        effect.transform.position = transform.position;
        SoundManager.Instance.PlaySound(Resources.Load<AudioClip>(NoteUnit.SoundPath));

        if (_active)
        {
            PoolManager.Instance.Release(TrackNoteCreator.TrackNotePrefabPath, gameObject);
            _active = false;
        }
    }
}
