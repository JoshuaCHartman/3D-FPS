using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;

    //crouch variables
    private Transform _lookRoot; // decrease player height
    private float _standHeight = 1.6f;
    private float _crouchHeight = 1f;
    private bool _isCrouching;

    private PlayerFootsteps _playerFootsteps; // audio 
    private float _sprintVolume = 1f;
    private float _crouchVolume = 0.1f;
    private float _walkVolumeMin = 0.2f, _walkVolumeMax = 0.6f;
    private float _walkStepDistance = 0.4f; // move 0.4 before playing sound
    private float _sprintStepDistance = 0.25f;
    private float _crouchStepDistance = 0.5f;

    // Stamina & UI Stamina
    private PlayerStatistics _playerStatistics;
    private float _sprintValue = 100f;
    public float sprintThreshold = 10f;
    private CharacterController _characterController; // to test if moving

    void Awake() // get references
    {
        _playerMovement = GetComponent<PlayerMovement>();

        _lookRoot = transform.GetChild(0); // get first child in hierarchy 

        _playerFootsteps = GetComponentInChildren<PlayerFootsteps>(); // playerfootsteps script in playeraudio object

        _playerStatistics = GetComponent<PlayerStatistics>();

        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        WalkStepDefault();
    }

    void WalkStepDefault()
    {
        _playerFootsteps.stepDistance = _walkStepDistance;
        _playerFootsteps.volumeMin = _walkVolumeMin;
        _playerFootsteps.volumeMax = _walkVolumeMax;
    }

    void Update()
    {
        Sprint();
        Crouch();
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_isCrouching)
            {
                //stand up
                _lookRoot.localPosition = new Vector3(0f, _standHeight, 0f);
                // local position is _lookRoot's transform position relative to Player object
                _playerMovement.speed = moveSpeed;
                WalkStepDefault();

                _isCrouching = false;

            }
            else
            {
                //crouch
                _lookRoot.localPosition = new Vector3(0f, _crouchHeight, 0f);
                // local position is _lookRoot's transform position relative to Player object
                _playerMovement.speed = moveSpeed;

                _playerFootsteps.stepDistance = _crouchStepDistance;
                _playerFootsteps.volumeMin = _crouchVolume;
                _playerFootsteps.volumeMax = _crouchVolume;

                _isCrouching = true;
            }
        }
    }

    void Sprint()
    {
        // check if have stamina, can sprint
        if (_sprintValue > 0f)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && !_isCrouching)
            {

                _playerMovement.speed = sprintSpeed;
                _playerFootsteps.stepDistance = _sprintStepDistance;
                _playerFootsteps.volumeMin = _sprintVolume; // sound louder when sprinting
                _playerFootsteps.volumeMax = _sprintVolume;
            }

        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && !_isCrouching)
        {
            _playerMovement.speed = moveSpeed;
            WalkStepDefault();
        }

        if (Input.GetKey(KeyCode.LeftShift) && !_isCrouching) // checks anytime left shift is pressed-
                                                              // if checked during sprint function, would only check once when initiall pressed
        {
            // test if moving & if so apply stamina decrease
            if (_characterController.velocity.sqrMagnitude > 0) // if any value of the vector3 (x,y,z) >0 Player is moving
            {
                // lose stamina at rate determined by sprintThreshold
                _sprintValue -= Time.deltaTime * sprintThreshold;

                // Check level & ensure _sprintvalue does not go below 0
                if (_sprintValue <= 0)
                {
                    _sprintValue = 0;

                    // reset sprint speed & sound
                    _playerMovement.speed = moveSpeed;
                    WalkStepDefault();
                }
                // display decrease stamina on bar
                _playerStatistics.DisplayStaminaStats(_sprintValue);
            }
        }
        else
        {
            if (_sprintValue != 100f) // if not full stamina
            {
                // regnerate stamina but at lower rate than lost
                _sprintValue += (sprintThreshold / 2f) * Time.deltaTime;

                // check stamina level & ensure not more than 100
                if (_sprintValue > 100f)
                {
                    _sprintValue = 100f;
                }

                // display regen stamina on bar
                _playerStatistics.DisplayStaminaStats(_sprintValue);
            }
        }
    }


}