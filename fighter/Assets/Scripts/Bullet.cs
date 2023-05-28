using UnityEngine;


internal class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _me;
    [SerializeField] private int damage;
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, _speed * Time.deltaTime, ~_me))
        {
            transform.position = hit.point;
            if (hit.transform.gameObject.TryGetComponent<Ship>(out var ship))
            {
                ship.ChangeHealth(-damage);
            }
            
        }
        transform.position += transform.forward * (_speed * Time.deltaTime);
    }
}