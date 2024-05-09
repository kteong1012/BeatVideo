using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

using UnityEngine;

public class SideNote : Note
{
    private TweenerCore<Vector3, Vector3, VectorOptions> _tween;
    private bool _active;

    public NoteUnit NoteUnit { get; set; }
    public override void Show(Vector2 canvasSize)
    {
        _active = true;
        var speed = NoteUnit.Speed;
        var direction = NoteUnit.Direction;
        var halfHeight = canvasSize.y / 2;
        var halfWidth = canvasSize.x / 2;
        var length = direction switch
        {
            NoteDirection.Left => halfWidth - NoteUnit.Position.x,
            NoteDirection.Right => NoteUnit.Position.x + halfWidth,
            NoteDirection.Down => halfHeight - NoteUnit.Position.y,
            NoteDirection.Up => NoteUnit.Position.y + halfHeight,
            _ => throw new System.ArgumentOutOfRangeException()
        };
        var flyTime = length / speed;
        _tween?.Kill();

        switch (direction)
        {
            case NoteDirection.Left:
            case NoteDirection.Right:
                _tween = transform.DOLocalMoveX(NoteUnit.Position.x, flyTime).SetEase(Ease.Linear);
                break;
            case NoteDirection.Up:
            case NoteDirection.Down:
                _tween = transform.DOLocalMoveY(NoteUnit.Position.y, flyTime).SetEase(Ease.Linear);
                break;
            default:
                throw new System.ArgumentOutOfRangeException();
        }

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
            PoolManager.Instance.Release(SideNoteCreator.SideNotePrefabPath, gameObject);
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
            PoolManager.Instance.Release(SideNoteCreator.SideNotePrefabPath, gameObject);
            _active = false;
        }
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }
}
