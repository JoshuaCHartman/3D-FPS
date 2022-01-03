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

        characterController.Move(moveDirection);


    }
}
