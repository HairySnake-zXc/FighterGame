using UnityEngine;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;
    [SerializeField] private string volumeName;

    public void ChangeVolume(float volume) => mixer.audioMixer.SetFloat(volumeName, Mathf.Lerp(-80, 0, volume));
}