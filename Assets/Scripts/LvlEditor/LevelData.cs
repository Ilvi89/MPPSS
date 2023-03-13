using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string name;
    public List<EnemyData> enemies = new();
    public Vector3 playerPosition;
    public Vector3 endPosition;
}

[Serializable]
public class EnemyData
{
    public string id;
    public int shipType;
    public Quaternion shipRotation;
    public float shipMoveSpeed;
    public List<Vector3> pathPoints = new();
}