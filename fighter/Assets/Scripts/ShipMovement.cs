using System.Collections.Generic;
using System.Collections;
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
    
    [SerializeField] private int maxEngineForce;
    [SerializeField] private int minEngineForce;

    [SerializeField] private bool _canControlShip;

    private float _thrustMultiplier = 3;
    private Vector3 _mainForce = new Vector3();
    private List<Vector3> forces = new List<Vector3>();
    private List<Vector3> localAngularForces = new List<Vector3>();

    public static float engineForceAsPercentage;
    private void Start()
    {
        Cursor.visible = false;
        _canControlShip = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(ThrowAwayOnCollision());
        Debug.Log(collision.GetContact(0).normal);
        _mainForce = collision.GetContact(0).normal * _mainForce.magnitude;
        _thrustMultiplier = 0;
    }

    private IEnumerator ThrowAwayOnCollision()
    {
        _canControlShip = false;
        yield return new WaitForSeconds(2);
        _canControlShip = true;
    }


    private void Update()
    {
        if (_canControlShip)
        {
            forces.Clear();
            localAngularForces.Clear();

            var target = _target.NormVectorFromForwardToTarget;
            localAngularForces.Add(new Vector3(-target.y * _angularVerticalSpeed, target.x * _angularHorizontalSpeed));

            if (Input.GetKey(KeyCode.A))
            {
                if (_angularForceQuotient > _rotationAroundMaxSpeed)
                    _angularForceQuotient = _rotationAroundMaxSpeed;
                else
                    _angularForceQuotient += _rotationAroundSpeed;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (_angularForceQuotient < -_rotationAroundMaxSpeed)
                    _angularForceQuotient = -_rotationAroundMaxSpeed;
                else
                    _angularForceQuotient -= _rotationAroundSpeed;
            }
            else
                _angularForceQuotient *= .9f;

            localAngularForces.Add(new Vector3(0, 0, _angularForceQuotient) * (1000 * Time.deltaTime));


            if (Input.GetKey(KeyCode.W))
            {
                if (_thrustMultiplier < maxEngineForce)
                    _thrustMultiplier += .2f;
                else
                    _thrustMultiplier = maxEngineForce;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (_thrustMultiplier > minEngineForce)
                    _thrustMultiplier -= .1f;
                else
                    _thrustMultiplier = minEngineForce;
            }

            var engineForce = _thrustMultiplier * transform.forward;
            forces.Add(engineForce);

            engineForceAsPercentage =
                (engineForce.magnitude - minEngineForce) / (maxEngineForce - minEngineForce);

            transform.Rotate(localAngularForces.Aggregate((e1, e2) => e1 + e2) * Time.deltaTime, Space.Self);
            _mainForce = forces.Aggregate((v1, v2) => v1 + v2);
        }
        transform.position += _mainForce * Time.deltaTime;
    }
}
