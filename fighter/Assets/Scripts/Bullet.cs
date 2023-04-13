using Unity.VisualScripting;
using UnityEngine;

internal class Bullet : MonoBehaviour
{
    public Vector3 Forward { get; set; }
    [SerializeField] private float _speed;


    private void Update()
    {
        transform.position += Forward * _speed * Time.deltaTime;
    }
}