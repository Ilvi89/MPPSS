using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PathLineDrawer))]
public class GhostPath : MonoBehaviour
{
    [SerializeField] public UnityEvent onRootRemove;
    [SerializeField] public UnityEvent onShipClick;

    [SerializeField] private GameObject pathPointGhost;
    [SerializeField] private GameObject shipGhost;

    private readonly List<GameObject> _points = new();

    private PathLineDrawer _pathLineDrawer;
    private GameObject _ship;

    private void Start()
    {
        _pathLineDrawer = GetComponent<PathLineDrawer>();

        AddPoint(transform.position);
        _pathLineDrawer.CreateLine(_points[^1].transform);

        _ship = Instantiate(shipGhost, transform.position, Quaternion.identity, transform);
    }

    public List<Vector3> GetPath()
    {
        return _points.Select(p => p.transform.position).ToList();
    }

    public void AddPoint(Vector2 pos)
    {
        var newPoint = Instantiate(pathPointGhost, pos, Quaternion.identity, transform);
        _points.Add(newPoint);

        SetShipRotation();

        var ghostPathPoint = newPoint.GetComponent<GhostPathPoint>();
        ghostPathPoint.onClick.AddListener(delegate
        {
            if (_points.Count == 1) onShipClick?.Invoke();
        });
        ghostPathPoint.onClickWithShift.AddListener(delegate { DeletePoint(newPoint); });
        ghostPathPoint.onDrop.AddListener(delegate { UpdatePoint(newPoint); });
    }


    private void SetShipRotation()
    {
        if (_points.Count <= 1) return;

        _pathLineDrawer.UpdateLine(_points[^1].transform);
        var direction = (_points[1].transform.position - _ship.transform.position)
            .normalized;
        _ship.transform.rotation = Quaternion.LookRotation(
            _ship.transform.forward,
            direction);
    }

    private void UpdatePoint(GameObject point)
    {
        if (_points.IndexOf(point.gameObject) == 0)
        {
            _ship.transform.position = point.transform.position;
            SetShipRotation();
        }

        _points.First(o => o == point).transform.position = point.transform.position;
        _pathLineDrawer.UpdateLine(_points.IndexOf(point.gameObject), point.transform);
    }

    private void DeletePoint(GameObject point)
    {
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
        }
    }
}