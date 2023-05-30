using UnityEngine;

public class Dammage : MonoBehaviour
{
    [SerializeField] private Canvas image;
    [SerializeField] private LightShip ship;

    private void Update() => image.enabled = ship.Health <= ship.MaxHealth * 0.35;
}
