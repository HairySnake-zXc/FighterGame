using System.Collections.Generic;
using UnityEngine;

public class SpaceGeneration : MonoBehaviour
{
    [SerializeField] private int _asteroidFieldCount;
    [SerializeField] private int _solarSystemCount;
    [SerializeField] private int _starDestroyersCount;
    [SerializeField] private float _sectorRadius;

    [SerializeField] private int _distance;
    [SerializeField] private GameObject _test;
    private IEnumerable<GameObject> _spawned = new List<GameObject>();

    private void Awake()
    {
        _spawned = ShpereRandomGeneration.SpawnPlanet(_sectorRadius, transform, _asteroidFieldCount + _solarSystemCount + _starDestroyersCount, _test, _distance);
    }
}
