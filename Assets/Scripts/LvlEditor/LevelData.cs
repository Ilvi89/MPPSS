using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelData
{
    public string name;
    public List<LevelShipsData> ships = new();
}

[Serializable]
public class LevelShipsData
{
    public Vector3 position;
    public Quaternion quaternion;
    public List<Vector3> points;
}