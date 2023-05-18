using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum State
    {
        Attacking,
        Idle,
        Escaping
    }

    [SerializeField] private Ship _ship;

    private void Update()
    {
        
    }
}
