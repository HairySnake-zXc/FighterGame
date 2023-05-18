using UnityEngine;

public class SmoothCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _depthOffset;
    [SerializeField] private float _verticalOffset;

    private void LateUpdate()
    {
        var point = _target.forward * -_depthOffset + _target.up * _verticalOffset;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, _target.position + point, 8 * Time.deltaTime);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, _target.rotation, 8 * Time.deltaTime);
    }
}
