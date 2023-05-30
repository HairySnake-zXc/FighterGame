using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image healthBarFilling;
    [SerializeField] private Ship ship;
    [SerializeField] private float damageIndicationTime;
    private void OnEnable()
    {
        ship.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        ship.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float valueAsPercentage)
    {
        healthBarFilling.fillAmount = valueAsPercentage;
        StartCoroutine(DamageAnimation());
    }

    private IEnumerator DamageAnimation()
    {
        healthBarFilling.color = Color.red;
        yield return new WaitForSeconds(damageIndicationTime);
        healthBarFilling.color = Color.white;
    }
}
