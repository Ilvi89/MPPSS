using System.Collections.Generic;
using UnityEngine;

public class PathLineDrawer : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private List<Vector2> _points;
    private GameObject _currentLine;

    private LineRenderer _lineRenderer;


    public void CreateLine(Transform start)
    {
        _currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, transform);
        _lineRenderer = _currentLine.GetComponent<LineRenderer>();
        _points.Clear();
        _points.Add(start.position);
        _points.Add(start.position);
        _lineRenderer.SetPosition(0, _points[0]);
        _lineRenderer.SetPosition(1, _points[1]);
    }

    public void UpdateLine(Transform newPoint)
    {
        _points.Add(newPoint.position);
        _lineRenderer.positionCount++;
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, (Vector2) newPoint.position);
    }

    public void UpdateLine(int index, Transform newPoint)
    {
        if (index == 0)
        {
            _lineRenderer.SetPosition(0, newPoint.position);
            _lineRenderer.SetPosition(1, newPoint.position);
            _lineRenderer.positionCount = _lineRenderer.positionCount - 1;
        }
        else
        {
            _lineRenderer.SetPosition(index + 1, newPoint.position);
        }
    }

    public void DeleteLine(int index)
    {
        if (index == 0)
        {
            _points.Clear();
            Destroy(_currentLine);
        }
        else
        {
            for (var i = index + 1; i < _lineRenderer.positionCount - 1; i++)
                _lineRenderer.SetPosition(i, _lineRenderer.GetPosition(i + 1));

            _lineRenderer.positionCount = _lineRenderer.positionCount - 1;
            _points.RemoveAt(index);
        }
    }
}