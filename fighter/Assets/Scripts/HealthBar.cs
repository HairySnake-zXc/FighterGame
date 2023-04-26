using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    [SerializeField] private Image _healthBarFilling;
    [SerializeField] Ship ship;

    private void Awake()
    {
        ship.HealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        ship.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float valueAsPercentage)
    {
        Debug.Log(valueAsPercentage);
        _healthBarFilling.fillAmount = valueAsPercentage;
        StartCoroutine(DamageAnimation());
    }

    private IEnumerator DamageAnimation()
    {
        _healthBarFilling.color = Color.red;
        yield return new WaitForSeconds(2);
        _healthBarFilling.color = Color.white;
    }
}
