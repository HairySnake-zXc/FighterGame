using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairMovement : MonoBehaviour

{
    public Vector3 NormVectorFromForwardToTarget => _directionToTarget / _maxRadius;
    
    [SerializeField] private Image _crosshair;
    private Vector3 _directionToTarget;
    private float _maxRadius = Screen.height / 3;

    void Update()
    {
        var currentAngleVector = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        if (currentAngleVector.magnitude < _maxRadius)
            _crosshair.transform.position = Input.mousePosition;
        else
            _crosshair.transform.position = currentAngleVector.normalized * _maxRadius
                + new Vector3(Screen.width / 2, Screen.height / 2, 0);
        _directionToTarget = _crosshair.transform.position - new Vector3(Screen.width / 2, Screen.height / 2, 0);
    }
}
