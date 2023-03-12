using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(PathLineDrawer))]
public class Editor : MonoBehaviour
{
    [Header("Require")] [SerializeField] private GhostShipPanelDropdown dropdown;

    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField speedField;

    [Space] [SerializeField] private UnityEvent onPathRemove;

    [SerializeField] private UnityEvent onPathSelect;

    [SerializeField] private GameObject pathGhost;

    [SerializeField] private LevelData _levelData = new();
    [SerializeField] private GhostPath _currentPath;
    [SerializeField] private string _levelName;
    private readonly Storage _storage = new();

    private bool _enabled;

    private void Start()
    {
        nameField.onValueChanged.AddListener(s => _levelName = s);
        speedField.onValueChanged.AddListener(s =>
        {
            var b = float.TryParse(s, out var val);
            if (b) SetShipSpeed(val);
        });
        dropdown.GetComponent<TMP_Dropdown>()
            .onValueChanged.AddListener(i => SetShipType(dropdown.GetShipData(i)));
    }

    private void LateUpdate()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mousePos2D = new Vector2(mousePos.x, mousePos.y);

        if (_currentPath is null && Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("LevelEditor"))
            {
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
                _currentPath.onPathUpdate.AddListener(delegate { UpdatePathData(_currentPath); });
            }
            else
            {
                _currentPath.AddPoint(new Vector3(worldPosition.x, worldPosition.y));
            }
        }
    }

    public void SetShipSpeed(float value)
    {
        _levelData.enemies.Find(data => data.id == _currentPath.Id).shipMoveSpeed = value;
    }

    public void SetShipType([CanBeNull] ShipData value)
    {
        if (value is null)
        {
            _levelData.enemies.Find(data => data.id == _currentPath.Id).shipData = dropdown.GetShipData(0);
        }

        _levelData.enemies.Find(data => data.id == _currentPath.Id).shipData = value;
    }


    public void LoadLevel()
    {
        _levelData = _storage.Load(_levelName);
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Disable()
    {
        _currentPath.EditingDeactivate();
        UpdatePathData(_currentPath);
        SetShipType(null);
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
        _levelData.enemies.Add(new EnemyData
        {
            id = pathId,
            pathPoints = new List<Vector3>()
        });
    }

    private void DeletePathData(string pathId)
    {
        var item = _levelData.enemies.First(data => data.id == pathId);

        _levelData.enemies.Remove(item);
    }

    private void UpdatePathData(GhostPath path)
    {
        _levelData.enemies.First(data => data.id == path.Id).pathPoints = path.GetPath();
        _levelData.enemies.First(data => data.id == path.Id).shipRotation = path.shipRotation;
    }
}