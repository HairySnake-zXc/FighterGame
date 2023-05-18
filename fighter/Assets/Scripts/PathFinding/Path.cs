using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Path
{
    public Path(List<Vector3Int> start, float length)
    {
        PathCollection = start;
        Length = length;
    }

    public List<Vector3Int> PathCollection { get; set; }

    public Vector3Int Last => PathCollection.Last();
    public float Length { get; private set; }
    public Path AddToPath(Vector3Int point)
    {
        var a = PathCollection.ToList();
        a.Add(point);
        return new Path(a, Length + (point - a[^1]).magnitude);
    }
}