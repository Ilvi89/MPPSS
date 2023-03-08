using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PathLineDrawer))]
public class GhostPath : MonoBehaviour
{
    [SerializeField] private GameObject pathPointGhost;
    [SerializeField] private GameObject shipGhost;

    private readonly List<GameObject> _points = new();
    private GameObject _ship;

    private PathLineDrawer _pathLineDrawer;

    public LevelShipsData GetShipData()
    {
        var data = new LevelShipsData
        {
            position = _ship.transform.position,
            quaternion = _ship.transform.rotation,
            points = _points.Select(p => p.transform.position).ToList()
        };
        return data;
    }
    private void Start()
    {
        _pathLineDrawer = GetComponent<PathLineDrawer>();
        
        AddPoint(transform.position);
        _pathLineDrawer.CreateLine(_points[^1].transform);
        
        _ship = Instantiate(shipGhost, transform.position, Quaternion.identity, transform);
    }

    public void AddPoint(Vector2 pos)
    {
        var newPoint = Instantiate(pathPointGhost, pos, Quaternion.identity, transform);
        _points.Add(newPoint);
        
        if (_points.Count > 1)
        {
            _pathLineDrawer.UpdateLine(_points[^1].transform);
            var direction = (_points[1].transform.position - _ship.transform.position)
                .normalized;
            _ship.transform.rotation = Quaternion.LookRotation(
                _ship.transform.forward,
                direction);
        }
    }
}