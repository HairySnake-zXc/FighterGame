using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ship : MonoBehaviour
{
    private float _health;
    public float Health => _health;

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException();
        _health -= Math.Min(_health, damage);
        if (damage <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        
    }
}
