using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PathLineDrawer))]
public class Editor : MonoBehaviour
{
    [SerializeField] private GameObject pathGhost;
    [SerializeField] private ShipData currentShipData;

    private readonly LevelData _levelData = new();
    private readonly Storage _storage = new();
    private GhostPath _currentPath;

    private bool _enabled;
    private readonly string _levelName = "Test";


    private void LateUpdate()
    {
        if (_enabled is false || EventSystem.current.IsPointerOverGameObject()) return;
        var screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var clickPoint = new Vector3(worldPosition.x, worldPosition.y, 0);
            if (_currentPath is null)
            {
                _currentPath = Instantiate(pathGhost, clickPoint, Quaternion.identity).GetComponent<GhostPath>();
                _currentPath.onRootRemove.AddListener(delegate
                {
                    Destroy(_currentPath);
                    _currentPath = null;
                    _enabled = false;
                });
                _currentPath.onShipClick.AddListener(delegate { Debug.Log("chose ship data pls"); });
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
        _levelData.enemies.Add(new EnemyData
        {
            shipData = currentShipData,
            pathPoints = _currentPath.GetPath()
        });


        _currentPath = null;
        _enabled = false;
    }

    public void Save()
    {
        _levelData.name = _levelName;
        _storage.Save(_levelData);
    }
}