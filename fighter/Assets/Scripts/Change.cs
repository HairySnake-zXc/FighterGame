using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Change : MonoBehaviour
{
    [SerializeField] private Text text;

    void Update()
    {
        text.text = ((int)(ShipMovement.engineForceAsPercentage * 100)).ToString() + '%';
        text.color = Math.Abs(ShipMovement.engineForceAsPercentage * 100 - 100) < Double.Epsilon ? Color.red : Color.white;
    }
}