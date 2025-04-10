using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CharControl : MonoBehaviour
{

    //character movements
    private CharacterController controller;
    //store our movement input
    private Vector3 playerMovementInput;
    //store velocity for gravity and jumping on y axis
    private Vector3 velocity;
    public static float walkSpeed = 5f;
    public bool isRunning;
    public float runningSpeed;
    public bool isCrouching = false;
    //magic gravity number
    private float gravity = -9.81f;
    public bool isGrounded;
    public float jumpForce;
    float currentSpeed;
    //ground checking
    //layer for ground detection
    public LayerMask groundLayer;
    //empty gameobject at the players feet
    public Transform groundCheck;
    //raycast distance for ground detection
    private float groundDistance = .3f;

    public GameObject respawnPos;

    //camera/mouse settings
    public float mouseSensitivity;
    //ref to our camera
    public Transform cameraTransform;
    //tracking camera vertical and hortizontal movement
    private float yrotation = 0;
    private float xrotation = 0;


    public static int playerHealth = 100;
    //distress goes up when you shoot good zombie --> speed goes down --> at 0 cant walk
    public static int distress = 0;

    //fufillment goes up when you shoot a bad zombie --> speed goes up
    public static int fufillment = 0;


    //health text stuff
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI distressText;
    public TextMeshProUGUI fufillText;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //we lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    void FixedUpdate()
    {
        //is more reliable for physics than collision enter and exit
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down,
        groundDistance);
        Debug.DrawRay(groundCheck.position, Vector3.down * groundDistance,
        Color.red);

    }


    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        //zInput gets players w or s input which is -1 or 1
        float zInput = Input.GetAxis("Vertical");

        //for character controller, you can just call velocity, this
       // direvtly helps with movement
        velocity = transform.forward * zInput * walkSpeed +
        transform.right * horizontalInput * walkSpeed + Vector3.up *
        velocity.y;

        CameraLook();
        MovePlayer();
        UpdateStatsText();


        if(playerHealth <= 0)
        {
          
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(4);

        }



    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Good Zombie" || collision.gameObject.tag == "Bad Zombie")
    //    {
    //        //health going down by two?? is it because enemy glithces on the player weirdly???

    //       Debug.Log("collision happend");
    //       playerHealth -= 1;
    //       Debug.Log("Player Health: " + playerHealth);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Good Zombie" && EnemyAI.dead == true)
        {
            distress += 5;
            walkSpeed -= .35f;
            Debug.Log("distress down by 10:" +  distress);  
        }


    }

    private void CameraLook()
    {
        //getting and assiging mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //when the mouse moves horizontally
        //we rotate around the y axis to look left and right
        yrotation += mouseX;
        //rotate the player left/right on y axis rotation
        transform.rotation = Quaternion.Euler(0f, yrotation, 0);
        //decrease xRotation when moving mouse up so camera tilts up
        //increase x rotation when moving cam down so cam tilts down
        xrotation -= mouseY;
        xrotation = Mathf.Clamp(xrotation, -90, 90); //prevents flipping
        cameraTransform.localRotation = Quaternion.Euler(xrotation, 0, 0);

    }

    private void MovePlayer()
    {
        isGrounded = controller.isGrounded;
        //if the player is on the ground reset gravity
        if (isGrounded)
        {
            //small downward force to keep us grounded
            velocity.y = -1;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        //move player on the x and z

        controller.Move(playerMovementInput * currentSpeed * Time.deltaTime);
        //apply vertical movement (gravity and jumping)
        controller.Move(velocity * Time.deltaTime);
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


    public void UpdateStatsText()
    {
        healthText.text = "Health: " + playerHealth.ToString();
        distressText.text = "Distress: " + distress.ToString();
        fufillText.text = "Fufillment: " + fufillment.ToString();
    }

    public void Health (int amount)
    {
        playerHealth -= amount;
        UpdateStatsText();


    }



}


