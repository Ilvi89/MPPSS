using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PathLineDrawer))]
public class Editor : MonoBehaviour
{
    [SerializeField] private GameObject shipGhost;
    [SerializeField] private GameObject pathGhost;
    [SerializeField] private GameObject pathPointGhost;

    private bool _enabled = false;
    private readonly List<GameObject> _currentPathPoints = new();
    private GameObject _currentPath;
    private GameObject _currentShip;

    private PathLineDrawer _pathLineDrawer;
    

    private readonly Storage _storage = new Storage();
    private readonly LevelData _levelData = new LevelData();


    private void Start()
    {
        _pathLineDrawer = GetComponent<PathLineDrawer>();
    }

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
                
                _currentPathPoints.Add(
                    Instantiate(pathPointGhost, clickPoint, Quaternion.identity, _currentPath.transform)
                );
                _pathLineDrawer.CreateLine(_currentPathPoints[^1].transform);

            }
            else
            {
                _currentPathPoints.Add(
                    Instantiate(pathPointGhost, new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity,
                        _currentPath.transform));
                _pathLineDrawer.UpdateLine(_currentPathPoints[^1].transform);
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
        var shipData = new LevelShipsData();
        shipData.position = _currentShip.transform.position;
        shipData.quaternion = _currentShip.transform.rotation;
        _levelData.ships.Add(shipData);
        
        _currentShip = null;
        _currentPath = null;
        _currentPathPoints.Clear();
        _enabled = false;
    }
    public void Save()
    {
        _levelData.name = "Test";
        _storage.Save(_levelData);
    }
}