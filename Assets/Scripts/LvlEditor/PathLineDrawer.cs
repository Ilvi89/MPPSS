using System;
using System.Collections.Generic;
using UnityEngine;

public class PathLineDrawer : MonoBehaviour
{
    [SerializeField] private GameObject linePrefab;
    private GameObject _currentLine;

    private LineRenderer _lineRenderer;
    [SerializeField]private List<Vector2> _points;


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
        _lineRenderer.SetPosition(_lineRenderer.positionCount - 1, (Vector2)newPoint.position);
    }
}