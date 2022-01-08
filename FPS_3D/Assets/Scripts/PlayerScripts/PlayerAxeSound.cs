using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAxeSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _axeSounds;

    void PlayAxeSound()
    {
        _audioSource.clip = _axeSounds[Random.Range(0, _axeSounds.Length)];
        _audioSource.Play();

    }
}
