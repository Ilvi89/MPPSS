using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShipMouseHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] private UnityEvent onClick;

    // [SerializeField] private TransformEvent onClickTransform;
    private Ship _ship;

    private void Awake()
    {
        _ship = GetComponent<Ship>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Debug.Log("Exit");
    }
}