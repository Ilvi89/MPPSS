using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class GhostPathPoint : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] public UnityEvent onDrop;
    [SerializeField] public UnityEvent onClickWithShift;
    [SerializeField] public UnityEvent onClick;

    private readonly float clickdelay = 0.5f;


    private float _clicked;
    private float _clicktime;


    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(mousePosition);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        onDrop?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            onClickWithShift?.Invoke();
            Debug.Log("click shift");
        }
        else
        {
            onClick?.Invoke();
            Debug.Log("click");
        }
    }
}