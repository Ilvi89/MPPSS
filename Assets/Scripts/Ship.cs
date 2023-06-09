﻿using System.ComponentModel;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Transform))]
[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour
{
    [SerializeField] private Path path;
    [SerializeField] private ShipData shipData;
    [SerializeField] public float shipMoveSpeed;

    [SerializeField] [DefaultValue(true)] private bool startFromFirstPoint;
    [SerializeField] [DefaultValue(false)] private bool smoothRotation;

    // Todo: Move to ShipData?
    [SerializeField] [Min(1)] private float radiusToDetect = 1;

    [SerializeField] public UnityEvent onCrash;

    private Transform _currentPoint;

    private Vector3 _direction;
    private Rigidbody2D _rigidbody2D;

    public float DirectionAngle => Quaternion.LookRotation(transform.forward, _direction).eulerAngles.z;
    public ShipData ShipData => shipData;

    private void SetUp()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentPoint = path.GetNextPointTransform();
        if (startFromFirstPoint)
        {
            _rigidbody2D.position = _currentPoint.position;
            _currentPoint = path.GetNextPointTransform();
            Quaternion targetRotation;
            if (path.IsCompleted)
            {
                targetRotation = Quaternion.LookRotation(transform.forward, _currentPoint.eulerAngles);
            }
            else
            {
                _direction = (_currentPoint.position - transform.position).normalized;
                targetRotation = Quaternion.LookRotation(transform.forward, _direction);
            }

            _rigidbody2D.SetRotation(targetRotation);
        }

        GetComponentInChildren<SpriteRenderer>().sprite = shipData.ShipSprite;
    }
    private void Awake()
    {
        if (path is null)
        {
            return;
        }
        SetUp();
    }

    private void FixedUpdate()
    {
        if (path.IsCompleted) return;
        Move();
        Rotate();
        if (Vector2.Distance(_rigidbody2D.position, _currentPoint.position) <= radiusToDetect)
            _currentPoint = path.GetNextPointTransform();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player")) onCrash.Invoke();
    }

    public void SetShipData(ShipData data)
    {
        shipData = data;
    }

    public void SetPath(Path p)
    {
        path = p;
        SetUp();
    }

    private void Move()
    {
        _rigidbody2D.MovePosition(
            _rigidbody2D.position + (Vector2) transform.up * (Time.fixedDeltaTime * shipMoveSpeed * 1.85f * 0.30f * 0.5f * 0.5f));
    }

    private void Rotate()
    {
        _direction = (_currentPoint.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(transform.forward, _direction);

        if (smoothRotation is not true || shipData.RotationSpeed == 0)
        {
            _rigidbody2D.SetRotation(targetRotation);
            return;
        }

        var rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, shipData.RotationSpeed);
        _rigidbody2D.MoveRotation(rotation);
    }
}