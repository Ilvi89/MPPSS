using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GetSideByVector))]
public class ShipPlane : MonoBehaviour
{
    // [SerializeField] private Ship ship;
    [SerializeField] private Image flag;
    [SerializeField] private Image lights;
    [SerializeField] private TMP_Text speed;

    [SerializeField] private GetSideByVector getSideByVector;
    [SerializeField] private SmoothFollow smoothFollow;


    private Ship _targetShip;
    [SerializeField] private ShipData _targetShipData;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        lights.sprite = _targetShipData.GetLight(getSideByVector.GetSide());
    }

    private void Show()
    {
        flag.sprite = _targetShipData.Flag;
        lights.sprite = _targetShipData.GetLight(getSideByVector.GetSide());
        speed.text = _targetShipData.DataMoveSpeed + " knots";
        gameObject.SetActive(true);
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ChangeShip(Ship ship)
    {
        if (ship == _targetShip)
        {
            if (isActiveAndEnabled) return;
            Show();
            return;
        }
        _targetShip = ship;
        _targetShipData = ship.ShipData;
        smoothFollow.SetTarget(_targetShip);
        getSideByVector.SetTarget(ship.transform);
        Hide();
        Show();
    }
}