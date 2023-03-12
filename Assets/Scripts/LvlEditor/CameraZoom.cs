using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomChange;
    [SerializeField] private float smoothChange;
    [SerializeField] private float minSize, maxSize;
    private Camera _cam;

    private void Start()
    {
        _cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
            _cam.orthographicSize -= zoomChange * Time.deltaTime * smoothChange;
        if (Input.mouseScrollDelta.y < 0)
            _cam.orthographicSize += zoomChange * Time.deltaTime * smoothChange;

        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize, minSize, maxSize);
    }
}