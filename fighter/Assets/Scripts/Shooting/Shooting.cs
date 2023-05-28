using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform[] _guns;

    [SerializeField] private Bullet _bullet;
    private float _heatPercentage;
    [SerializeField] private Text text;
    private void Start()
    {
        StartCoroutine(Shoot());
        StartCoroutine(Heat());
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (_heatPercentage >= 100)
            {
                text.color = Color.red;
                text.text = "OVERHEAT!";
                yield return new WaitForSeconds(2f);
                text.color = Color.white;
            }
            if (Input.GetKey(KeyCode.Mouse0))
            {
                foreach (var item in _guns)
                {
                    var bullet = Instantiate(_bullet.gameObject, item.position, gameObject.transform.rotation);
                    Destroy(bullet.gameObject, 4f);
                }
                _heatPercentage += 2000f * Time.deltaTime;
                yield return new WaitForSeconds(.15f);
            }

            text.text = $"heat: {(int)_heatPercentage}";
            yield return 0;
        }
    }

    private IEnumerator Heat()
    {
        while (true)
        {
            if (_heatPercentage > 0)
                _heatPercentage -= 50f * Time.deltaTime;
            else
                _heatPercentage = 0;
            yield return null;
        }
    }

}
