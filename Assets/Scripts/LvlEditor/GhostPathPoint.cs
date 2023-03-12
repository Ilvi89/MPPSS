using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GhostPathPoint : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] public UnityEvent onDrop;
    [SerializeField] public UnityEvent onClickWithShift;
    [SerializeField] public UnityEvent onClick;
    private bool _isRoot;
    public bool IsEditing { get; set; } = true;

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetMouseButton(1) || IsEditing == false) return;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (IsEditing == false) return;
        onDrop?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift))
            onClickWithShift?.Invoke();
        else
            onClick?.Invoke();
    }

    public void SetRoot()
    {
        _isRoot = true;
    }

    public bool IsRoot()
    {
        return _isRoot;
    }
}