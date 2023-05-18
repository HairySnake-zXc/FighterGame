using UnityEngine;


internal class Bullet : MonoBehaviour
{
    public Vector3 Forward { get; set; }
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _me;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, _speed * Time.deltaTime, ~_me))
        {
            transform.position = hit.point;
            if (hit.transform.gameObject.TryGetComponent<Ship>(out var ship))
            {
                ship.ChangeHealth(-5);
            }
            
        }
        transform.position += Forward * _speed * Time.deltaTime;
    }
}