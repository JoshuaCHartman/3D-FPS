using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerFootsteps : MonoBehaviour

{
    private AudioSource _footstepSound;

    [SerializeField] private AudioClip[] _footstepClip; // array of clips, current length 4

    private CharacterController _characterController; // determine whether on ground/ moving to play correct sounds

    /*[HideInInspector]*/
    public float volumeMin, volumeMax;

    [SerializeField] private float _accumulatedDistance; //how far move before play sound
    
    /*[HideInInspector]*/
    public float stepDistance;


    // Start is called before the first frame update
    void Awake()
    {
        // set references to game objects
        _footstepSound = GetComponent<AudioSource>();
        _characterController = GetComponentInParent<CharacterController>(); // audio script attached to child. must move up the hierarchy to get character controller.

    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    void CheckToPlayFootstepSound()
    {
        if (!_characterController.isGrounded)
            return;

        if (_characterController.velocity.sqrMagnitude > 0) // if any value of the vector3 (x,y,z) >0
        {
            //_accumulatedDistance = how far to move (step,walk,sprint,moving crouch) until play step sound
            _accumulatedDistance += (Time.deltaTime);

            if (_accumulatedDistance > stepDistance)
            {
                //AudioClip randomClip;

                //randomClip = _footstepClip[Random.Range(0, _footstepClip.Length)];
                //_footstepSound.clip = randomClip;

                _footstepSound.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
                _footstepSound.clip = _footstepClip[Random.Range(0, _footstepClip.Length)]; // random range selection to pick clip in the array. Int range is incluse of x, exclusive of y. Float is inclusive

                _footstepSound.Play();

                _accumulatedDistance = 0f;

            }
        }
        else
        {
            _accumulatedDistance = 0f;
        }
        

    }
}
