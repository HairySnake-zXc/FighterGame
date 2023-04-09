using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform[] _guns;

    [SerializeField] private Bullet _bullet;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                foreach (var item in _guns)
                {
                    var bullet = Instantiate(_bullet.gameObject, item.position, gameObject.transform.rotation);
                    bullet.GetComponent<Bullet>().Forward = transform.forward;
                    Destroy(bullet.gameObject, 4f);
                }
                yield return new WaitForSeconds(.15f);
            }
            yield return 0;
        }
    }

}
