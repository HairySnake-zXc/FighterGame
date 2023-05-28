using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Ship ship;
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private Transform target;
    private enum State
    {
        Attacking,
        Idle,
        Escaping
    }

    [SerializeField] private Ship _ship;

    private void Update()
    {
        
    }

    private float EstimateState()
    {
        var lowHealthPenalty = ship.MaxHealth / ship.Health * 10000;
        var enemyDistance = Vector3.Distance(transform.position, ship.transform.position);
        return -lowHealthPenalty;
    }

    private State ChooseBehaviour()
    {
        return State.Idle;
    }
}
