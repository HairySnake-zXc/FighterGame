using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] Image _crosshair;
    [SerializeField] Transform _ship;
    private float _angleRotationConstant = Screen.height / 3;
    private Vector3 _anglePoint;
    private float _angularForce;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private float _rotationAroundSpeed;

    private float _velocity = 3;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        var mousePosition = Input.mousePosition;
        var currentAngleVector = mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        if (currentAngleVector.magnitude < _angleRotationConstant)
            _crosshair.transform.position = mousePosition;
        else
            _crosshair.transform.position = currentAngleVector.normalized * _angleRotationConstant
                + new Vector3(Screen.width / 2, Screen.height / 2, 0);

        var direction = _crosshair.transform.position - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        _ship.Rotate(new Vector3(-direction.y, direction.x) * Time.deltaTime * _angularSpeed, Space.Self);



        if (Input.GetKey(KeyCode.A))
        {
            if (_angularForce > _rotationAroundSpeed)
                _angularForce = _rotationAroundSpeed;
            else
                _angularForce += 20f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (_angularForce < -_rotationAroundSpeed)
                _angularForce = -_rotationAroundSpeed;
            else
                _angularForce -= 20f;
        }
        _angularForce *= .95f;
        _ship.Rotate(new Vector3(0, 0, _angularForce) * Time.deltaTime, Space.Self);


        if (Input.GetKey(KeyCode.W))
        {
            if (_velocity < 15)
                _velocity += .2f;
            else
                _velocity = 15;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            if (_velocity > 3)
                _velocity -= .1f;
            else
                _velocity = 3;
        }


        _ship.transform.position += _ship.forward * _velocity * Time.deltaTime;
    }
}
