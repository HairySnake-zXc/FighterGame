using UnityEngine;

public class DamageSound : MonoBehaviour
{
    [SerializeField] private Ship ship;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;
    
    private void OnEnable() => ship.HealthChanged += OnHealthChanged;

    private void OnDisable() => ship.HealthChanged -= OnHealthChanged;

    private void OnHealthChanged(float valueAsPercentage) => source.PlayOneShot(clip);
}
