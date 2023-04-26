using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnginePower : MonoBehaviour
{
    [SerializeField] private Image barFilling;

    private void Update()
    {
        barFilling.fillAmount = ShipMovement.engineForceAsPercentage;
    }

}
