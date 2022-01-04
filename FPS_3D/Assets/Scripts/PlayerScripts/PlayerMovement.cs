using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;

    public float speed = 5f;
    private float gravity = 20f;

    public float jumpForce = 10f;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>(); // reference to Player Character Controller component

    }
    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        // GetAxis - incremental values vs GetAxisRaw - whole integer (-1,0,1)
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f, Input.GetAxis(Axis.VERTICAL)); // (x,y,z) using TagHolder helper script

        moveDirection = transform.TransformDirection(moveDirection); // change from local to global directions
        moveDirection *= speed * Time.deltaTime;

        ApplyGravity(); // below - test gravity for jump

        characterController.Move(moveDirection); // charactercontroller.move requires a V3 to be passed. moveDirection defined as V3 above, and will change every frame.

    }

    void ApplyGravity()
    {
        //if (characterController.isGrounded)
        //{
            verticalVelocity -= gravity * Time.deltaTime; // apply gravity so player remains planted and does not bounce/fall/float

            //jump
            PlayerJump();
              
        //}
        //else
        //{
        //    verticalVelocity -= gravity * Time.deltaTime; // not on ground, apply gravity
        //}

        moveDirection.y = verticalVelocity * Time.deltaTime; // multiply by deltaTime to smooth out and reduce vertical velocity

    }

    private void PlayerJump()
    {
        if (characterController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;

        }
    }





}
