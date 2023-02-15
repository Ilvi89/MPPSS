using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GetSideByVector))]
public class ShipPlane : MonoBehaviour
{
    // [SerializeField] private Ship ship;
    [SerializeField] private Image flag;
    [SerializeField] private Image lights;

    [SerializeField] private GetSideByVector getSideByVector;


    private Ship _targetShip;
    private ShipData _targetShipData;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        lights.sprite = _targetShipData.GetLight(getSideByVector.GetSide());
    }

    private void Show(Ship ship)
    {
        flag.sprite = _targetShipData.Flag;
        lights.sprite = _targetShipData.GetLight(getSideByVector.GetSide());
        gameObject.SetActive(true);
        gameObject.SetActive(true);
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ChangeShip(Ship ship)
    {
        if (ship == _targetShip) return;
        _targetShip = ship;
        _targetShipData = ship.ShipData;
        getSideByVector.SetTarget(ship.transform);
        Hide();
        Show(ship);
    }
}