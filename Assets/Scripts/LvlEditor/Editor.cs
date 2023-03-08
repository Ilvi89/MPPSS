using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PathLineDrawer))]
public class Editor : MonoBehaviour
{
    [SerializeField] private GameObject pathGhost;
    
    private readonly LevelData _levelData = new();
    private readonly Storage _storage = new();
    private GameObject _currentPath;

    private bool _enabled;


    private void LateUpdate()
    {
        if (_enabled is false || EventSystem.current.IsPointerOverGameObject()) return;
        var screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Input.GetMouseButtonDown(0))
        {
            var clickPoint = new Vector3(worldPosition.x, worldPosition.y, 0);
            if (_currentPath is null)
                _currentPath = Instantiate(pathGhost, clickPoint, Quaternion.identity);
            else
                _currentPath.GetComponent<GhostPath>().AddPoint(new Vector3(worldPosition.x, worldPosition.y));
        }
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        var ghostPath = _currentPath.GetComponent<GhostPath>();
        var shipData = ghostPath.GetShipData();
        _levelData.ships.Add(shipData);
        
        
        _currentPath = null;
        _enabled = false;
    }

    public void Save()
    {
        _levelData.name = "Test";
        _storage.Save(_levelData);
    }
}