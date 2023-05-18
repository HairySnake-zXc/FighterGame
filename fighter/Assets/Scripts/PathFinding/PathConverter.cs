using UnityEngine;
using Random = UnityEngine.Random;

public class PathConverter : MonoBehaviour
{
    [SerializeField] private NavigationMap _map;
    private Path _path;
    private Vector3Int _currentPoint;

    private void OnDrawGizmos()
    {
        if (_path != null)
        {
            var center = new Vector3Int(_map.MapWithNotFreeCells.GetLength(0),
                _map.MapWithNotFreeCells.GetLength(1),
                _map.MapWithNotFreeCells.GetLength(2)) / 2;
            for (int i = 1; i < _path.PathCollection.Count; i++)
            {
                Gizmos.DrawLine(_map.transform.position + _path.PathCollection[i - 1] - center,
                    _map.transform.position + _path.PathCollection[i] - center);
            }
        }
    }

    public void MakePath()
    {
        Vector3Int a;
        do
        {
            a = new Vector3Int(Random.Range(0, _map.MapWithNotFreeCells.GetLength(0) - 1),
                Random.Range(0, _map.MapWithNotFreeCells.GetLength(1) - 1),
                Random.Range(0, _map.MapWithNotFreeCells.GetLength(2) - 1));
        } while (_map.MapWithNotFreeCells[a.x, a.y, a.z]);

        _path = Pathfinding.Astar(_currentPoint, a, _map.MapWithNotFreeCells);
    }
}