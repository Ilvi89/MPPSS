using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipPlane : MonoBehaviour
{
    // [SerializeField] private Ship ship;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text dir;
    [SerializeField] private Image lights;
    [SerializeField] private Image flag;
    [SerializeField] private Compass compass;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 curDir;
    [SerializeField] private float sum;
    [SerializeField] private float sum2;
    [SerializeField] private float sum2_;

    [SerializeField] private bool _isActive;

    private Ship _ship;
    private ShipData _shipData;

    private void Awake()
    {
        Hide();
    }

    private void FixedUpdate()
    {
        // TODO: thing about dynamic params
        if (_isActive)
        {
            compass.UpdateRotation(_ship.DirectionAngle);
            GetCorrectLight();
        }
    }

    private void Show(Ship ship)
    {
        // TODO: Update data method
        _ship = ship;
        _shipData = ship.ShipData;
        title.text = "_shipData.ShipType.ToString()";
        flag.sprite = _shipData.Flag;
        GetCorrectLight();
        _isActive = true;
        gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void GetCorrectLight()
    {
        // TODO: remake. use SideChecker scene 


        sum = Quaternion.Angle(_ship.transform.rotation, player.rotation);
        
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = _ship.transform.position - transform.position;

        if (Vector3.Dot(forward, toOther) < 0)
        {
            print("The other transform is behind me!");
        }
        
        
        if (sum is <= 45 or > 0)
        {
            if (Vector3.Dot(_ship.transform.position, player.position) < 0)
            {
                lights.sprite = _shipData.LightFront;
                dir.text = _shipData.LightFront.name;
            }
            else
            {
                lights.sprite = _shipData.LightBack;
                dir.text = _shipData.LightBack.name;
            }
        }
        else
        {
            if (Vector3.Dot(_ship.transform.position, player.position) > 0)
            {
                lights.sprite = _shipData.LightBack;
                dir.text = _shipData.LightBack.name;
            }
            else
            {
                lights.sprite = _shipData.LightFront;
                dir.text = _shipData.LightFront.name;
            }
        }
    }

    private void Hide()
    {
        _isActive = false;
        gameObject.SetActive(false);
    }

    public void ChangeShip(Ship ship)
    {
        if (ship == _ship) return;
        Hide();
        Show(ship);
    }
}