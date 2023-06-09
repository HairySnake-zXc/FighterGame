using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ShpereRandomGeneration : MonoBehaviour
{
    public static List<GameObject> SpawnPlanet(
        float spawnRadius,
        Transform spawnPoint,
        int spawnCount,
        GameObject planet,
        int _distance)
    {
        var planetsAndSpawnPoint = new HashSet<Vector3> { spawnPoint.position };
        var list = new List<GameObject>();
        for (var i = 0; i < spawnCount; i++)
        {
            var randomPosition = spawnPoint.position + Random.insideUnitSphere * spawnRadius;
            var isIntersect =
                planetsAndSpawnPoint.Any(planetVector =>
                    (planetVector - randomPosition).magnitude < _distance); //������������ �� �������

            if (isIntersect)
                continue;

            planetsAndSpawnPoint.Add(randomPosition);
            var a = Instantiate(planet, randomPosition, Quaternion.identity, parent: spawnPoint);
            var b = Random.Range(1, 5);
            a.transform.localScale = new Vector3(b, b, b);
            list.Add(a);
        }

        return list;
    }
}