using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Transform[] guns;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Vector3 _target;
    [SerializeField] private float _rotationSpeed;
    private State _state = State.Idle;
    private bool _canAttack = false;

    private void Start()
    {
        StartCoroutine(Fire());
    }


    private void Update()
    {
        Vector3 target = new Vector3();
        switch (_state)
        {
            case State.Attacking:
                target = _target;
                break;
            case State.Idle:
                target = transform.forward * 1000 + transform.position;
                break;
        }

        var targetPosition = target - tower.position;
        var rotateToEnemy = Vector3.ProjectOnPlane(targetPosition, transform.up);
        tower.forward = Vector3.Slerp(tower.forward, rotateToEnemy, _rotationSpeed * Time.deltaTime);
        var barrelPosition = target - barrel.position;
        var aimToEnemy = Vector3.ProjectOnPlane(barrelPosition, tower.right);
        if (Vector3.SignedAngle(tower.forward, aimToEnemy, barrel.right) <= 0)
            barrel.forward = Vector3.Slerp(barrel.forward, aimToEnemy, _rotationSpeed * Time.deltaTime);
        if (_state == State.Attacking)
        {
            if (Vector3.Angle(barrelPosition, barrel.forward) < 10)
            {
                _canAttack = true;
            }
            else
            {
                _canAttack = false;
            }
        }
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            if (_canAttack && _state == State.Attacking)
            {
                for (var i = 0; i < 3; i++)
                {
                    foreach (var point in guns)
                    {
                        var obj = Instantiate(bullet, point.position, point.rotation);
                        Destroy(obj, 6f);
                    }

                    yield return new WaitForSeconds(.5f);
                }

                yield return new WaitForSeconds(3f);
            }

            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent<ShipMovement>(out var ship))
        {
            _state = State.Attacking;
            _target = other.transform.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<ShipMovement>(out var ship))
        {
            _state = State.Idle;
        }
    }
}