using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private RectTransform _line;
    [SerializeField] private float _rotation;

    private void FixedUpdate()
    {
        _line.rotation = Quaternion.Euler(new Vector3(0, 0, _rotation));
    }

    public void UpdateRotation(float value)
    {
        _rotation = value;
    }
}