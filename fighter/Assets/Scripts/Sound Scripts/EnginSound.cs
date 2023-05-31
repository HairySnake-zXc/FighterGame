using UnityEngine;

public class EnginSound : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    private void Update()
    {
        source.volume = ShipMovement.engineForceAsPercentage;
    }
}
