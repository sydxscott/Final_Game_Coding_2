using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSRBPlayer : MonoBehaviour
{
    //ground your player
    //jump
    //start a game manager
    //restart


    private Rigidbody rb;
    public float walkSpeed = 5f;

    //how fast is our camera moving
    public float mouseSensitivity;
    //we need a ref to our camera
    public Transform cameraTransform;
    //tracking camera vertical and horizontal movement 
    private float yRotation = 0;
    private float xRotation = 0;

    public bool isCrouching = false;
    //var for crouch speed

    public bool isRunning;
    public float runningSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //we lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //zInput gets players w or s input which is -1 or 1
        float zInput = Input.GetAxis("Vertical");

        //transform.forward (0, 0, 1)
        rb.velocity = transform.forward * zInput * walkSpeed +
            transform.right * horizontalInput * walkSpeed + Vector3.up * rb.velocity.y;

        CameraLook();

        if (Input.GetKeyDown(KeyCode.E))
        {
            //slipping the bool so we can toggle it
            isCrouching = !isCrouching;
            Crouch();
        }

        float currentSpeed;
        if (isRunning)
        {
            currentSpeed = runningSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

    }

    private void CameraLook()
    {
        //getting and assiging mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //when the mouse moves horizontally
        //we rotate around the y axis to look left and right
        yRotation += mouseX;
        //rotate the player left/right on y axis rotation
        transform.rotation = Quaternion.Euler(0f, yRotation, 0);

        //decrease xRotation when moving mouse up so camera tilts up
        //increase x rotation when moving cam down so cam tilts down
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90); //prevents flipping

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);

    }

    private void Crouch()
    {
        if (isCrouching)
        {
            transform.localScale = new Vector3(1, .5f, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }
}