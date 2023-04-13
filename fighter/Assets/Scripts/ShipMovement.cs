using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public CrosshairMovement _target;
    private float _angularForceQuotient;
    [SerializeField] private float _angularVerticalSpeed;
    [SerializeField] private float _angularHorizontalSpeed;
    [SerializeField] private float _rotationAroundSpeed;
    [SerializeField] private float _rotationAroundMaxSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _speedAddMultiplier;
    [SerializeField] private float _speedReduceMultiplier;

    private float _thrustMultiplier = 3;
    private Vector3 _mainForce = new Vector3();
    private Vector3 _thrust;


    private void Start()
    {
        _thrust = transform.forward;
        Cursor.visible = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.impulse);
        _thrustMultiplier = Mathf.Min(-_thrustMultiplier, -10);
    }

    private void Update()
    {
        var forces = new List<Vector3>();
        var localAngularForces = new List<Vector3>();

        var target = _target.NormVectorFromForwardToTarget;
        localAngularForces.Add(new Vector3(-target.y * _angularVerticalSpeed, target.x * _angularHorizontalSpeed));

        if (Input.GetKey(KeyCode.A))
        {
            if (_angularForceQuotient > _rotationAroundSpeed)
                _angularForceQuotient = _rotationAroundSpeed;
            else
                _angularForceQuotient += _rotationAroundMaxSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (_angularForceQuotient < -_rotationAroundSpeed)
                _angularForceQuotient = -_rotationAroundSpeed;
            else
                _angularForceQuotient -= -_rotationAroundMaxSpeed;
        }
        else
            _angularForceQuotient *= .95f;

        localAngularForces.Add(new Vector3(0, 0, _angularForceQuotient) * Time.deltaTime);


        if (Input.GetKey(KeyCode.W))
        {
            if (_thrustMultiplier < 15)
                _thrustMultiplier += .2f;
            else
                _thrustMultiplier = 15;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_thrustMultiplier > 3)
                _thrustMultiplier -= .1f;
            else
                _thrustMultiplier = 3;
        }

        var engineForce = _thrustMultiplier * _thrust;
        forces.Add(engineForce);
        _mainForce = forces.Aggregate((v1, v2) => v1 + v2);
        transform.position += _mainForce * Time.deltaTime;
    }
}
