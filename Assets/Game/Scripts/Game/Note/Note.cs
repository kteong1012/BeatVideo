using Game.Cfg.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
public abstract class Note : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public ResourcesGameObjectPool Pool { get; set; }
    public abstract void Show();

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnClickNote();
    }
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        OnEnterNote();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        OnExitNote();
    }

    protected virtual void OnClickNote()
    {
    }

    protected virtual void OnEnterNote()
    {
    }

    protected virtual void OnExitNote()
    {
    }

}
