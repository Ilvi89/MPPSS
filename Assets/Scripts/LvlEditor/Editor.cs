using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PathLineDrawer))]
public class Editor : MonoBehaviour
{
    [SerializeField] private UnityEvent onPathRemove;
    [SerializeField] private UnityEvent onPathSelect;
    
    [SerializeField] private GameObject pathGhost;
    [SerializeField] private ShipData currentShipData;

    [SerializeField] private  LevelData _levelData;
    private readonly string _levelName = "Test";
    private readonly Storage _storage = new();
    [SerializeField] private GhostPath _currentPath;
    private GhostPath _currentPathTmp;

    private bool _enabled;


    private void LateUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (_currentPath is null && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("LevelEditor")) {
                Enable();
                _currentPath = hit.collider.gameObject.GetComponentInParent<GhostPath>();
                _currentPath.EditingActivate();
                onPathSelect?.Invoke();
            }
        }
        
        if (_enabled is false || EventSystem.current.IsPointerOverGameObject()) return;
        var screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var clickPoint = new Vector3(worldPosition.x, worldPosition.y, 0);
            if (_currentPath is null)
            {
                _currentPath = Instantiate(pathGhost, clickPoint, Quaternion.identity).GetComponent<GhostPath>();
                _currentPath.Id = Guid.NewGuid().ToString();
                AddNewPathData(_currentPath.Id);
                
                _currentPath.onRootRemove.AddListener(delegate
                {
                    DeletePathData(_currentPath.Id);
                    Destroy(_currentPath);
                    _currentPath = null;
                    _enabled = false;
                    onPathRemove?.Invoke();
                });
                _currentPath.onPathUpdate.AddListener(delegate
                {
                    UpdatePathData(_currentPath);
                });
            }
            else
            {
                _currentPath.AddPoint(new Vector3(worldPosition.x, worldPosition.y));
            }
        }
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _currentPath.EditingDeactivate();
        UpdatePathData(_currentPath);
        _currentPath = null;
        _enabled = false;
    }
    

    public void Save()
    {
        _levelData.name = _levelName;
        _storage.Save(_levelData);
    }


    private void AddNewPathData(string pathId)
    {
        _levelData.enemies.Add(new EnemyData()
        {
            id = pathId,
            shipData = currentShipData,
            pathPoints = new List<Vector3>()
        });
    }

    private void DeletePathData(string pathId)
    {
        Debug.Log(_levelData.enemies.Count);
        Debug.Log(pathId);
        var item = _levelData.enemies.First(data => data.id == pathId);

        _levelData.enemies.Remove(item);
    }
    
    private void UpdatePathData(GhostPath path)
    {
        _levelData.enemies.First(data => data.id == path.Id).pathPoints = path.GetPath();
    }
}