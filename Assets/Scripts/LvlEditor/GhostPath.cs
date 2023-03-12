using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PathLineDrawer))]
public class GhostPath : MonoBehaviour
{
    [SerializeField] public UnityEvent onRootRemove;
    [SerializeField] public UnityEvent onShipClick;
    [SerializeField] public UnityEvent onPathUpdate;

    [SerializeField] private GameObject pathPointGhostPrefab;
    [SerializeField] private GameObject shipGhostPrefab;

    private readonly List<GameObject> _points = new();
    private bool _isEditingActivate;

    private PathLineDrawer _pathLineDrawer;
    private GameObject _ship;
    public String Id;

    public Quaternion shipRotation;

    private void Start()
    {
        _pathLineDrawer = GetComponent<PathLineDrawer>();

        AddPoint(transform.position);
        _pathLineDrawer.CreateLine(transform);
        EditingActivate();

        _ship = Instantiate(shipGhostPrefab, transform.position, Quaternion.identity, transform);
    }

    public void EditingActivate()
    {
        _pathLineDrawer.SetActiveColor(); ;
        _points.ForEach(p => p.GetComponent<GhostPathPoint>().IsEditing = true);
        _isEditingActivate = true;
    }

    public void EditingDeactivate()
    {
        _pathLineDrawer.SetInactiveColor();
        _points.ForEach(p => p.GetComponent<GhostPathPoint>().IsEditing = false);
        _isEditingActivate = false;
    }

    public List<Vector3> GetPath()
    {
        return _points.Select(p => p.transform.position).ToList();
    }

    public void AddPoint(Vector2 pos)
    {
        var newPoint = Instantiate(pathPointGhostPrefab, pos, Quaternion.identity, transform);
        _points.Add(newPoint);
        
    
        var ghostPathPoint = newPoint.GetComponent<GhostPathPoint>();
        ghostPathPoint.onClickWithShift.AddListener(delegate { DeletePoint(newPoint); });
        ghostPathPoint.onDrop.AddListener(delegate { UpdatePoint(newPoint); });
        
            
        

        if (_points.Count == 1)
        {
            ghostPathPoint.SetRoot();
        }
        else if(_points.Count == 2)
        {
            SetShipRotation();
        }
        
        if (_points.Count <= 1) return;
        _pathLineDrawer.UpdateLine(_points[^1].transform);


    }


    private void SetShipRotation()
    {
        var direction = (_points[1].transform.position - _ship.transform.position)
            .normalized;
        _ship.transform.rotation = Quaternion.LookRotation(
            _ship.transform.forward,
            direction);
        shipRotation = _ship.transform.rotation;
    }

    private void UpdatePoint(GameObject point)
    {
        // if (!_isEditingActivate) return;
        var index = _points.IndexOf(point.gameObject);
        if (index < 2)
        {
            if (index == 0) _ship.transform.position = point.transform.position;
            SetShipRotation();
        }

        _points.First(o => o == point).transform.position = point.transform.position;
        _pathLineDrawer.UpdateLine(_points.IndexOf(point.gameObject), point.transform);
        onPathUpdate?.Invoke();
    }

    private void DeletePoint(GameObject point)
    {
        if (!_isEditingActivate) return;
        var index = _points.IndexOf(point.gameObject);
        if (index == 0)
        {
            _pathLineDrawer.DeleteLine(0);
            _points.ForEach(o => Destroy(o));
            Destroy(_ship);
            Destroy(gameObject);
            onRootRemove?.Invoke();
        }
        else
        {
            _points.RemoveAt(index);
            _pathLineDrawer.DeleteLine(index);
            Destroy(point);
            onPathUpdate?.Invoke();
        }
    }
}