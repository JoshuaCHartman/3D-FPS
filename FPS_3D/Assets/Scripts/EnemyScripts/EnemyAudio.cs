using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _screamClip, _deathClip;
    [SerializeField] private AudioClip[] _attackClips;

    void Awake()
    {
        // get references
        _audioSource = GetComponent<AudioSource>();

    }
    public void PlayScreamSound()
    {
        _audioSource.clip = _screamClip;
        _audioSource.Play();
    }
    public void PlayAttackSound()
    {
        _audioSource.clip = _attackClips[Random.Range(0, _attackClips.Length)];
        _audioSource.Play();
    }

    public void PlayDeathSound()
    {
        _audioSource.clip = _deathClip;
        _audioSource.Play();
    }

}
