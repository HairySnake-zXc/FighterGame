using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private enum State
    {
        Idle,
        Attacking
    }
    [SerializeField] private Transform tower;
    [SerializeField] private Transform barrel;
    private State _state = State.Idle;
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed;

    private Vector3 startPos;

    private void Start()
    {
        startPos = barrel.forward;
    }

    private void Update()
    {
        switch (_state)
        {
            case State.Attacking:
                
                break;
            case State.Idle:
                break;
            
            
        }
        var targetPosition = _target.position - tower.position;
        var rotateToEnemy = Vector3.ProjectOnPlane(targetPosition, transform.up);
        tower.forward = Vector3.Slerp(tower.forward, rotateToEnemy, _rotationSpeed * Time.deltaTime);
        var barrelPosition = _target.position - barrel.position;
        var aimToEnemy = Vector3.ProjectOnPlane(barrelPosition, tower.right);
        if (Vector3.SignedAngle(tower.forward, aimToEnemy, barrel.right) <= 0)
            barrel.forward = Vector3.Slerp(barrel.forward, aimToEnemy, _rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ShipMovement>(out var ship))
        {
            _state = State.Attacking;
            _target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ShipMovement>(out var ship))
        {
            _state = State.Idle;
            _target = transform;
        }
    }
}
