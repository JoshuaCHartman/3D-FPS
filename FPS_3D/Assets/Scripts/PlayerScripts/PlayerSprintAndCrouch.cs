using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float sprintSpeed = 10f;
    public float moveSpeed = 5f;
    public float crouchSpeed = 2f;

    //crouch variables
    private Transform lookRoot; // decrease player height
    private float standHeight = 1.6f;
    private float crouchHeight = 1f;
    private bool isCrouching;


    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();

        lookRoot = transform.GetChild(0); // get first child in hierarchy 


    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                //stand up
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                // local position is lookRoot's transform position relative to Player object
                playerMovement.speed = moveSpeed;
                isCrouching = false;


            }
            else
            {
                //crouch
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                // local position is lookRoot's transform position relative to Player object
                playerMovement.speed = moveSpeed;
                isCrouching = true;
            }
        }
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = sprintSpeed;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = moveSpeed;

        }
    }


}
