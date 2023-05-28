using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemyMovement : MonoBehaviour
{
    private Path _currentPath;
    [SerializeField] private NavigationMap _zone;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Transform _target;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform[] _guns;
    [SerializeField] private Vector3Int startPosition;
    private int _index;

    private void Start()
    {
        transform.position = WorldPositionFromNode(startPosition);
        _currentPath = CreatePathToRandomPoint(_zone.MapWithNotFreeCells, startPosition);
        StartCoroutine(MoveTowardsPath());
    }

    
    private IEnumerator MoveTowardsPath()
    {
        while (true)
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
            {
                _index++;
                if (Random.Range(0, 11) < .1)
                {
                    yield return ShootPlayer();
                }
            }
            transform.forward =
                Vector3.Slerp(transform.forward, toTarget, _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator ShootPlayer()
    {
        var timer = 0f;
        StartCoroutine(Shoot( 3));
        while (true)
        {
            timer += Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, (_target.position - transform.position).normalized, _rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * (_speed * Time.deltaTime);
            yield return null;
            if (timer > 3)
                yield break;
            
        }
    }

    private IEnumerator Shoot(int charges)
    {
        yield return new WaitForSeconds(.3f);
        for (int i = 0; i < charges; i++)
        {
            foreach (var point in _guns)
            {
                var bullet = Instantiate(_bullet.gameObject, point.position, point.transform.rotation);
                Destroy(bullet.gameObject, 4f);
            }

            yield return new WaitForSeconds(1f);
            
        }
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
                Random.Range(0, map.GetLength(1) - 1),
                Random.Range(0, map.GetLength(2) - 1));
        } while (map[a.x, a.y, a.z]);

        return Pathfinding.Astar(startPoint, a, map);
    }
}
