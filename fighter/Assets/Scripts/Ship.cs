using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ship : MonoBehaviour
{
    [SerializeField] private int _health;

    [SerializeField] private float _speed;

    [SerializeField] private KeyCode _keyCode;
    
    private int _currentHealth;
    public int Health => _health;
    
    public float Speed => _speed;
    
    public event Action<float> HealthChanged; 
    
    void Start()
    {
        _currentHealth = _health;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            ChangeHealth(-10);
        }
    }
    
    private void ChangeHealth(int value)
    {
        _currentHealth += value;

        if (_currentHealth <= 0)
        {
            Death();
        }
        else
        {
            var currentHealthAsPercentage = (float)_currentHealth / _health;
            HealthChanged?.Invoke(currentHealthAsPercentage);
        }
    }

    private void Death()
    {
        HealthChanged?.Invoke(0);
        Debug.Log("YOU ARE DEAD");
    }
}
