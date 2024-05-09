using UnityEngine;
using UnityEngine.EventSystems;
public abstract class Note : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public abstract void Show(Vector2 canvasSize);

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
