using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ShipMouseHandler : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    [SerializeField] public UnityEvent onClick;

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