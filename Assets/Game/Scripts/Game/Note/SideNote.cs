using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Game.Cfg.Game;
using UnityEngine;

public class SideNote : Note
{
    private static ResourcesGameObjectPool _clickEffectPool = new ResourcesGameObjectPool("Effects/Click");
    private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

    public SideNoteUnit NoteUnit { get; set; }
    public override void Show()
    {
        var speed = NoteUnit.Speed;
        var direction = NoteUnit.Direction;
        var length = direction switch
        {
            SideNoteDirection.Left => 360f - NoteUnit.Position.x,
            SideNoteDirection.Right => NoteUnit.Position.x,
            _ => 0f
        };
        var flyTime = length / speed;
        _tween?.Kill();
        _tween = transform.DOLocalMoveX(NoteUnit.Position.x, flyTime);
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
        effect.Pool = _clickEffectPool;
        effect.transform.position = transform.position;
        Pool.Release(gameObject);
    }

    private void OnDestroy()
    {
        _tween?.Kill();
    }
}
