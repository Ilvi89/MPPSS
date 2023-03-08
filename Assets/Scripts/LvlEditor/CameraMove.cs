using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Camera _cam;
    private Vector3 _difference;

    private bool _drag;

    private Vector3 _origin;
    private Vector3 _resetCamera;

    private void Start()
    {
        _cam = GetComponent<Camera>();
        _resetCamera = _cam.transform.position;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            _difference = _cam.ScreenToWorldPoint(Input.mousePosition) - _cam.transform.position;
            if (_drag == false)
            {
                _drag = true;
                _origin = _cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _drag = false;
        }

        if (_drag) _cam.transform.position = _origin - _difference;
    }
}