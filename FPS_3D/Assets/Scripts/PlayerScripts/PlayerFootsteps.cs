using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerFootsteps : MonoBehaviour

{
    private AudioSource footstepSound;

    [SerializeField] private AudioClip[] footstepClip; // array of clips, current length 4

    private CharacterController characterController; // determine whether on ground/ moving to play correct sounds

    /*[HideInInspector]*/
    public float volumeMin, volumeMax;

    public float accumulatedDistance; //how far move before play sound
    /*[HideInInspector]*/
    public float stepDistance;


    // Start is called before the first frame update
    void Awake()
    {
        // set references to game objects
        footstepSound = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>(); // audio script attached to child. must move up the hierarchy to get character controller.

    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootstepSound();
    }

    private void CheckToPlayFootstepSound()
    {
        if (!characterController.isGrounded)
            return;

        if (characterController.velocity.sqrMagnitude > 0) // if any value of the vector3 (x,y,z) >0
        {
            //accumulatedDistance = how far to move (step,walk,sprint,moving crouch) until play step sound
            accumulatedDistance += Time.deltaTime;

            if (accumulatedDistance > stepDistance)
            {
                //AudioClip randomClip;

                //randomClip = footstepClip[Random.Range(0, footstepClip.Length)];
                //footstepSound.clip = randomClip;
                footstepSound.clip = footstepClip[Random.Range(0, footstepClip.Length)]; // random range selection to pick clip in the array. Int range is incluse of x, exclusive of y. Float is inclusive

                footstepSound.volume = UnityEngine.Random.Range(volumeMin, volumeMax);
                footstepSound.Play();

                accumulatedDistance = 0f;

            }

            else
            {
                accumulatedDistance = 0f;
            }
        }

    }
}
