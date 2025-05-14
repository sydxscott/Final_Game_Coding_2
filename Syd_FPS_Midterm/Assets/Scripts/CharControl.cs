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
    public Vector3 playerMovementInput;
    //store velocity for gravity and jumping on y axis
    private Vector3 velocity;
    public static float walkSpeed = 10f;
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


    public static int playerHealth;
    //distress goes up when you shoot good zombie --> speed goes down --> at 0 cant walk
    public static int distress;

    //fufillment goes up when you shoot a bad zombie --> speed goes up
    public static int fufillment;


    //health text stuff
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI distressText;
    public TextMeshProUGUI fufillText;
    public TextMeshProUGUI ammoText;


    //vars for dashing 
    public float maxDashDuration = .02f;
    public float dashDistance = 10f;
    public float dashCoolDown = 10f;
    public bool canDash = true;
    private bool isDashing = false;

    public HealthBar healthBar;
    public DistressBar distressBar;
    public int currentDistress;
    public int maxDistress;
    
 


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        //we lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        playerHealth = 100;
        distress = 0;   
        fufillment = 0;


        healthBar.SetMaxHealth(100);
        distressBar.SetInitialDistress(0);

     

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

        //CameraLook();
        MovePlayer();
        UpdateStatsText();
        MaxStat(distress);
        MaxStat(fufillment);


        if(Input.GetKey(KeyCode.LeftShift) && canDash && playerMovementInput == Vector3.zero)
        {
            Debug.Log("start dash");
            StartCoroutine(Dash());
        }


        if(playerHealth <= 0)
        {
          
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(4);

        }

    

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Good Zombie" && EnemyAI.dead == true)
        {
            distress += 10;
            //distress++;
            Debug.Log("distress increased");
            distressBar.SetDistress(distress);
            Debug.Log("Distress: " + distress.ToString());
            walkSpeed = 2f;
            Debug.Log("distress down by 10:" +  distress);

            //maxHealth += _health;
            //increases max value on the health bar by 1 (corresponds to the number of the button increase
            //healthBar.IncreaseMaxValue(1);
            //sets the bar to update the current health to the max value
            //healthBar.SetHealth(currentHealth);
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Good Zombie" && EnemyAI.dead == true)
        {

            walkSpeed = 10f;
            Debug.Log("return to normal speed:" + walkSpeed);
        }
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

   
    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

       // Vector3 dashDirection = playerMovementInput.normalized;

        Vector3 dashDirection = transform.forward;

        float dashSpeed = dashDistance/ maxDashDuration;

        print(dashSpeed);

        float startTime = Time.time;

        

        while (Time.time < startTime + maxDashDuration)
        {
            controller.Move(dashDirection * dashSpeed * Time.deltaTime);
            Debug.Log(dashDirection);
            yield return null;
        }

        isDashing = false;
        Debug.Log("Dash ended starting cooldom");

        yield return new WaitForSeconds(dashCoolDown);

        canDash = true;
        Debug.Log("dash cooldown is over");

    }


    public void UpdateStatsText()
    {
        healthText.text = "Health: " + playerHealth.ToString();
        distressText.text = "Distress: " + distress.ToString();
        fufillText.text = "Fufillment: " + fufillment.ToString();
        ammoText.text = "Ammo: " + Weapon.numberOfBullets.ToString() + " /12";

    }

    public void Health (int amount)
    {
        playerHealth -= amount;
        healthBar.SetHealth(playerHealth);

        UpdateStatsText();


    }

    public void MaxStat(int stat)
    {
        int statMax = 100;

        if(stat> statMax)
        {
            stat = statMax;
            Debug.Log(stat);
        }
    }

}


