using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyMoving : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other);
    }

    private Path _currentPath;
    [SerializeField] private NavigationMap _zone;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3Int startPosition;
    private int _index;

    private void Start()
    {
        transform.position = WorldPositionFromNode(startPosition);
        _currentPath = CreatePathToRandomPoint(_zone.MapWithNotFreeCells, startPosition);
    }

    private void Update()
    {
        if (_index == _currentPath.PathCollection.Count - 1)
        {
            _currentPath = CreatePathToRandomPoint(_zone.MapWithNotFreeCells, _currentPath.Last);
            _index = 0;
        }
        var next = WorldPositionFromNode(_currentPath.PathCollection[_index]);
        var normal = _currentPath.PathCollection[_index + 1] - _currentPath.PathCollection[_index];
        var transformVar = transform;
        var forward = transformVar.forward;
        transform.position += _speed * Time.deltaTime * forward;
        var toTarget = next - transformVar.position;
        if (Vector3.Dot(normal, -toTarget) > 0)
            _index++;
        transform.forward =
            Vector3.Slerp(transform.forward, toTarget, _rotationSpeed * Time.deltaTime);
    }

    private Vector3 WorldPositionFromNode(Vector3Int node)
    {
        return _zone.transform.position + (node - _zone.Center);
    }

    private Path CreatePathToRandomPoint(bool[,,] map, Vector3Int startPoint)
    {
        Vector3Int a;
        do
        {
            a = new Vector3Int(Random.Range(0, map.GetLength(0) - 1),
                Random.Range(0, map.GetLength(0) - 2),
                Random.Range(0, map.GetLength(0) - 3));
        } while (map[a.x, a.y, a.z]);

        return Pathfinding.Astar(startPoint, a, map);
    }
}
