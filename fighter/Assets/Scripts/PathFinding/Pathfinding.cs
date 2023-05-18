using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour
{
    private static List<Vector3Int> directions = new List<Vector3Int>()
    {
        new Vector3Int(0, 0, 1),
        new Vector3Int(0, 1, 0),
        new Vector3Int(1, 0, 0),
        new Vector3Int(1, 1, 0),
        new Vector3Int(1, 0, 1),
        new Vector3Int(0, 1, 1),
        new Vector3Int(1, 1, 1),

        new Vector3Int(0, 0, -1),
        new Vector3Int(0, -1, 0),
        new Vector3Int(-1, 0, 0),
        new Vector3Int(-1, -1, 0),
        new Vector3Int(-1, 0, -1),
        new Vector3Int(0, -1, -1),
        new Vector3Int(-1, -1, -1),

        new Vector3Int(-1, 0, 1),
        new Vector3Int(1, 0, -1),

        new Vector3Int(-1, 1, 1),
        new Vector3Int(-1, 1, 0),
        new Vector3Int(-1, 1, -1),
        new Vector3Int(0, 1, -1),
        new Vector3Int(1, 1, -1),

        new Vector3Int(-1, -1, 1),
        new Vector3Int(0, -1, 1),
        new Vector3Int(1, -1, 1),
        new Vector3Int(1, -1, 0),
        new Vector3Int(1, -1, -1)
    };

    
    private void Start()
    {
    }

    public static Path Astar(Vector3Int start, Vector3Int end, bool[,,] map)
    {
        var closed = new HashSet<Vector3Int>();
        var open = new PriorityQueue<Path, float>();
        open.Enqueue(new Path(new List<Vector3Int> { start }, 0), 0);
        while (open.Count > 0)
        {
            var path = open.Dequeue();
            var pathLast = path.Last;
            if (closed.Contains(pathLast))
                continue;
            if (pathLast == end)
                return path;
            closed.Add(pathLast);
            foreach (var neighbour in directions.Select(e => e + pathLast))
            {
                if (neighbour.x >= map.GetLength(0) || neighbour.x < 0
                                                    || neighbour.y >= map.GetLength(1) || neighbour.y < 0
                                                    || neighbour.z >= map.GetLength(2) || neighbour.z < 0)
                    continue;
                if (map[neighbour.x, neighbour.y, neighbour.z])
                    continue;
                var newPath = path.AddToPath(neighbour);
                open.Enqueue(newPath, newPath.Length + Vector3Int.Distance(neighbour, end));
            }
        }

        throw new Exception("Путь не найден");
    }
}