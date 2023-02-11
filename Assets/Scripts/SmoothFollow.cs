using System;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public float damping;
    private Vector2 _velocity = Vector2.zero;

    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (target is null) return;
        Vector2 movePosition = target.position + offset;
        Vector3 t = Vector2.SmoothDamp(transform.position, movePosition, ref _velocity, damping);
        t.Set(t.x, t.y, offset.z);
        transform.position = t;
    }

    private void OnEnable()
    {
        transform.position = target.position + offset;
    }

    public void SetTarget(Ship ship)
    {
        target = ship.transform;
    }
}