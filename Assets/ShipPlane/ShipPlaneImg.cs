using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipPlaneImg : MonoBehaviour
{
    [SerializeField] private Image img;
    [SerializeField] private TMP_Text speed;
    [SerializeField] private Image sound;
    [SerializeField] public AudioSource soundSource;
    
    [SerializeField] private GetSideByVector getSideByVector;
    [SerializeField] private SmoothFollow smoothFollow;

    private Ship _targetShip;
    private ShipData _targetShipData;
    private LvlMode Mode => LevelManager.Instance?.lvlMode ?? LvlMode.Day;

    private void Awake()
    {
        Hide();
    }

    private void Update()
    {
        if (Mode == LvlMode.Night)
        {
            img.sprite = _targetShipData.GetLight(getSideByVector.GetSide());
        }
    }

    private void Show()
    {
        sound.gameObject.SetActive(false);
        if (Mode == LvlMode.Day)
        {
            img.sprite = _targetShipData.Flag;
        } else if (Mode == LvlMode.Night)
        {
            img.sprite = _targetShipData.GetLight(getSideByVector.GetSide());
        }
        else if (Mode == LvlMode.Fog)
        {
            sound.gameObject.SetActive(true);
            sound.sprite = _targetShipData.SoundSprite;
            soundSource.clip = _targetShipData.SoundClip;
        }
        
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