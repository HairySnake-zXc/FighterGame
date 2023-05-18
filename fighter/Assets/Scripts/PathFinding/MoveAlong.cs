using UnityEngine;
using Random = UnityEngine.Random;

public class MoveAlong : MonoBehaviour
{ 
    [SerializeField] private NavigationMap map;
    [SerializeField] private Vector3Int startPoint;
    private int _index = 1;
    private Path currentPath;
    [SerializeField] private float speed;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (currentPath != null)
        {
            var center = new Vector3Int(map.MapWithNotFreeCells.GetLength(0),
                map.MapWithNotFreeCells.GetLength(1),
                map.MapWithNotFreeCells.GetLength(2)) / 2;
            for (int i = 1; i < currentPath.PathCollection.Count; i++)
            {
                Gizmos.DrawLine(map.transform.position + currentPath.PathCollection[i - 1] - center,
                    map.transform.position + currentPath.PathCollection[i] - center);
            }
        }
    }
    private void Start()
    {
        currentPath = CreatePathToRandomPoint(map.MapWithNotFreeCells);
    }

    private void Update()
    {
        var center = new Vector3Int(map.MapWithNotFreeCells.GetLength(0),
            map.MapWithNotFreeCells.GetLength(1),
            map.MapWithNotFreeCells.GetLength(2)) / 2;
        transform.LookAt(currentPath.PathCollection[_index] + map.transform.position - center);
        startPoint = currentPath.PathCollection[_index];
        var pointToMove = startPoint + map.transform.position - center;
        var position = transform.position;
        position += (pointToMove - position).normalized * (speed * Time.deltaTime);
        transform.position = position;
        if (Vector3.Distance(pointToMove, transform.position) < .1f)
        {
            _index++;
        }
        if (_index == currentPath.PathCollection.Count)
        {
            startPoint = currentPath.PathCollection[_index - 1];
            currentPath = CreatePathToRandomPoint(map.MapWithNotFreeCells);
            _index = 1;
        }
    }

    private Path CreatePathToRandomPoint(bool[,,] map)
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
