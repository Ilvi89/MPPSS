using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Editor : MonoBehaviour
{
    [SerializeField] private GameObject shipGhost;
    [SerializeField] private PathGhost pathGhost;
    [SerializeField] private GameObject pathPointGhost;

    private bool _enabled = false;
    private readonly List<GameObject> _currentPathPoints = new();
    private PathGhost _currentPath;
    private GameObject _currentShip;

    private void LateUpdate()
    {
        if (_enabled is false || EventSystem.current.IsPointerOverGameObject()) return;
        var screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Input.GetMouseButtonDown(0))
        {
            var clickPoint = new Vector3(worldPosition.x, worldPosition.y, 0);
            if (_currentPathPoints.Count == 0)
            {
                _currentShip = Instantiate(shipGhost, clickPoint, Quaternion.identity);
                _currentPath = Instantiate(
                    pathGhost,
                    new Vector3(worldPosition.x, worldPosition.y, 0),
                    Quaternion.identity
                );
                _currentPath.pathType = PathType.Path;

                _currentPathPoints.Add(
                    Instantiate(pathPointGhost, clickPoint, Quaternion.identity, _currentPath.transform)
                );
                ;
            }
            else
            {
                _currentPathPoints.Add(
                    Instantiate(pathPointGhost, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity,
                        _currentPath.transform));
                if (_currentPathPoints.Count > 1)
                {
                    var direction = (_currentPathPoints[1].transform.position - _currentShip.transform.position)
                        .normalized;
                    _currentShip.transform.rotation = Quaternion.LookRotation(
                        _currentShip.transform.forward,
                        direction);
                }
            }
        }
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _enabled = false;
        // Save
        _currentShip = null;
        _currentPath = null;
        _currentPathPoints.Clear();
    }
}