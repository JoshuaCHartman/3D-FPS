using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform playerRoot, lookRoot; // in inspector drop Player and LookRoot objects onto these variables
                                                             // Transform = postion, rotation, scale of an object
                                                             // playerRoot = y axis / _lookRoot = x axis
    [SerializeField] private bool invert;

    [SerializeField] private bool canUnlock = true; // mouse cursor, ESC can lock/unlock cursor

    [SerializeField] private float sensitivity = 5f;

    [SerializeField] private int smoothSteps = 10;

    [SerializeField] private float smoothWeight = 0.4f;

    [SerializeField] private float rollAngle = 10f;
    [SerializeField] private float rollSpeed = 3f;

    [SerializeField] private Vector2 defaultLookLimits = new Vector2(-70f, 80f);

    private Vector2 lookAngles;
    private Vector2 currentMouseLook;
    private Vector2 smoothMove;

    private float currentRollAngle;

    private int lastLookFrame;

    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;



    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock cursor to center of game window
        // ESC will free and make visible
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround(); // look around only when cursor locked
        }
    }

    private void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None; // unlock cursor when press ESC
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;  //Re-lock
            }
        }
    }

    void LookAround() // rotate game object based on where moved mouse
    {
        mouseX = Input.GetAxis(MouseAxis.MOUSE_X);
        mouseY = Input.GetAxis(MouseAxis.MOUSE_Y);
        //Debug.Log("Mouse Y is :" + mouseY + "Mouse X is :" + mouseX);

        currentMouseLook = new Vector2(mouseY, mouseX);
        // mouse input "reversed" because motion is AROUND axis

        lookAngles.x += currentMouseLook.x * sensitivity * (invert ? 1f : -1f); // invert to check player input
        lookAngles.y += currentMouseLook.y * sensitivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);
        // math function to use defaults limits ("clamp") if values from mouse input out of range
        // max look up & down, otherwise would be upside down

        //currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.MOUSE_X) 
        //                  * rollAngle, Time.deltaTime * rollSpeed); 

        //                  interpolate between a&b by t Mathf.Lerp(a,b,t)
        //                  slowly goes from a to b value in time specified
        // makes "dizzyness" / "drunkeness" => decrease rollAngle to decrease or set to 0
        // Currently not using
        
        currentRollAngle = 0f;

        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
        // Quaternion = rotations / Euler = rotate around z, x, y in that order

    }
}
