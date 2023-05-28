using UnityEngine;
using System;

public class Ship : MonoBehaviour
{
    [SerializeField] private int _health;

    [SerializeField] private float _speed;

    [SerializeField] private KeyCode _keyCode;

    [SerializeField] private DeathScreen screen;

    [SerializeField] private GameObject explosion;
    
    private int _currentHealth;
    public int Health => _currentHealth;
    public int MaxHealth => _health;
    
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
    
    public void ChangeHealth(int value)
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
        if (gameObject.TryGetComponent<ShipMovement>(out var a))
            screen.gameObject.SetActive(true);
        var b = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(b, 4f);
        Destroy(gameObject);
    }
}
