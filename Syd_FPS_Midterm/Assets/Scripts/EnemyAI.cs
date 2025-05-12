using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using Unity.VisualScripting;
using System.Runtime.InteropServices;

public class EnemyAI : MonoBehaviour
{
    //
    public enum EnemyState { Idle, Patrol, Chase, Attack,}
    private EnemyState currentState;

    //references 
    private Transform player;
    private NavMeshAgent agent;

    //patrol settings 
    public Transform[] patrolPoints;
    private int currentPatolIndex;

    //AI settings 
    public string enemyType;
    public int enemyHealth;
    private float speed;
    public float detectionRange;
    public float attackRange;
    public float attackCoolDown;
    public int attackDamage;
    

    public static bool dead;

    float lastAttackTime;

    // Reference to player script
    CharControl charContorlScript;


    //What happens to the body after killed 
    public float deapawnTime = 10f;

    private Rigidbody rb;
    public float rotationSpeed;

    SpriteBilboard spriteBilboard;

    public DistressBar distressBar;
    public FufillmentBar fufillmentBar;

 

    // good fahsion zombies --> parent a circel plane  or do on draw gizmo??? and particals for slow range --> add circle on trigger collider to impact health and speed   --> 
    // do rhis is switch statement and have a tag that is goodfashionzombie and have the if statement be if the tag is a goodzombie and is in dead state 



    // Start is called before the first frame update
    void Start()
    {
        spriteBilboard = GetComponent<SpriteBilboard>();

        charContorlScript = GameObject.FindGameObjectWithTag("Player").GetComponent<CharControl>();

        fufillmentBar.SetInitialFufill(0);

        //fufillmentBar = GameObject.FindGameObjectWithTag("Fufillment Bar");
       

        lastAttackTime =-attackCoolDown;

        //radius.SetActive(false);
        //Debug.Log("spotlight found: " +radius);

        agent = GetComponent<NavMeshAgent>();

        //load enemydata from json
        LoadEnemyData(enemyType);

        agent.speed = speed;


        currentState = EnemyState.Patrol; // start with ememy patrolling 

        MoveToNextPatrolPoint();

        rb = GetComponent<Rigidbody>(); 

        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        

    }



    // Update is called once per frame
    void Update()
    {

        //Debug.Log($"Enemy State: {currentState} | Distance to Player{Vector3.Distance(transform.position,player.position)} | Speed: {agent.speed} | has path: {agent.hasPath}");


        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //switch statement like multiplu choice decision maker 
        //check a variable and deiced what code to run based off the var value 

        //determining hwat behavoir the enemy should perfrom based off of its current state 

        switch (currentState)
        {
            case EnemyState.Idle:
                IdleBehavior();
               
                //break makesure program doent check other cases once match is found 
                break;

           
            case EnemyState.Patrol:
                PatrolBehavior();
                // enemy detiction will which to chse 
                if (distanceToPlayer<= detectionRange) ChangeState(EnemyState.Chase);
             
                break;

            case EnemyState.Chase:
                ChaseBehavior();
                if(distanceToPlayer<= attackRange) ChangeState(EnemyState.Attack);
                else if (distanceToPlayer> detectionRange) ChangeState(EnemyState.Patrol);
               
                break;

            case EnemyState.Attack:
                AttackBehavior();
                if(distanceToPlayer > attackRange) ChangeState(EnemyState.Chase);
           
                break;
        }

    }

    void Distress()
    {
        //Debug.Log("Dead true/false" + dead);

        if (dead && gameObject.tag == "Good Zombie")
        {
            CharControl.distress += 3;
            //CharControl.distressBar.SetDistress(CharControl.distress);
            distressBar.SetDistress(CharControl.distress);

            //Debug.Log("distress went up, ff =" + CharControl.distress);
            CharControl.walkSpeed -= 0.15f;

        }

    }

    void Fufillment()
    {
        //Debug.Log("Dead true/false" + dead);
        if (dead && gameObject.tag == "Bad Zombie")
        {
            EnemySpawner.badZombies -= 1;
            CharControl.fufillment += 3;
            fufillmentBar.SetFufill(CharControl.fufillment);

            CharControl.walkSpeed += 0.5f;
            //Debug.Log("fufillment went up, ff =" +  CharControl.fufillment);

        }

    }


    void ChangeState (EnemyState newState)
    {
        currentState = newState;
    }
    
    void IdleBehavior()
    {
        // you can add an animation if you want 
        //sound 
    }

    void PatrolBehavior()
    {
        //enemy follows path to taret 
        //it waits until it reaches tat patrolpoint 
        //once it reaches th pont it move s to bext location 
        // pathpending is true if unity is sitll calculating the path 
        // if its false patjh has been fully caulculated and em=beny us novin e
        // if enemy is colse enoygh to patrol point within .5 it moves to next one 


        if(!agent.enabled || !agent.isOnNavMesh) return;

        if(!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToNextPatrolPoint();
        }

    } 

    
    private void MoveToNextPatrolPoint()
    {
        if(patrolPoints.Length == 0)
        {
            return;
        }

        //.dot set destintion moves to next patrol point 
        agent.SetDestination(patrolPoints[currentPatolIndex].position);
        //update the curent indec and wrap it around 
        currentPatolIndex = (currentPatolIndex + 1) % patrolPoints.Length;

    }

    void ChaseBehavior()
    {
        if (!agent.enabled || !agent.isOnNavMesh) return;


        agent.SetDestination(player.position);
        //Debug.Log("chase Called");
    }

    void AttackBehavior()
    {
        //chahing the enimies color, sound, animation, spawing sphres, etc.
        if(Time.time >= lastAttackTime + attackCoolDown)
        {
            lastAttackTime = Time.time;
            //Debug.Log("enemy attacked player");
            //logic to reduce player hearth healthscript.getcomponent.losinghealth
            
            if (enemyHealth > 0)
            {
                charContorlScript.Health(attackDamage);

            }

            


        }

    }

    void DeathStateBad()
    {
        //stops from moving
        agent.enabled = false;

        dead = true;
       
        StartCoroutine(DespawnTime());

        StartCoroutine(ZombieFall());

        Fufillment();
        GetComponent<LootBag>().InstantiateLoot(transform.position); 

        //Debug.Log("enemy fallen");
    }

    void DeathStateGood()
    {
        //fall over 
        //make radius and stuff 
        //slow stuff --> will be in ontrigger enter 

        //stops from moving 
        Light radius;
        //radis = GetComponentInChildren<>
        radius = GetComponentInChildren<Light>();
        //radius = GameObject.FindGameObjectWithTag("Radius");
        Collider trigger = GetComponentInChildren<SphereCollider>();

        agent.enabled = false;
        dead = true;
        Distress();

        StartCoroutine(DespawnTime());

        StartCoroutine(ZombieFall());
        //Debug.Log("enemy fallen");

        //radius.SetActive(true);
        radius.enabled = true;   
        
        trigger.enabled = true;
   
       // Debug.Log("light on");
       // Debug.Log("fallen and rotated to:" + transform.rotation);
    }
    

    IEnumerator ZombieFall()
    {
        spriteBilboard.enabled =false;

        Quaternion newRotation = Quaternion.Euler(0, 0, 90);
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y - 0.6f, transform.position.z);
        float elapseTime = 0;
        float durration = 10f;

        while (elapseTime < durration)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,newRotation, elapseTime/durration );
            transform.position = Vector3.Lerp(transform.position, newPosition, elapseTime/durration);
            elapseTime += Time.deltaTime;
            yield return null;

            Debug.Log("zombuie fall");
        }

        transform.position = newPosition;  
        transform.rotation = newRotation;

        yield return new WaitForSeconds(1);
        //Destroy(gameObject);
    }


    IEnumerator DespawnTime()
    {
        while (dead == true)
        {
            yield return new WaitForSeconds(deapawnTime);
            //spawn enmey function here
            Destroy(gameObject);
            //Debug.Log("despawn");

        }
    }

    //add differtn states 
    // if distance is greater than attack range fire projectles 
    //stealth --> ignore play if crouth behind color 
    //

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {

            enemyHealth -= 1;
            // Debug.Log("enemy Health: " + enemyHealth + "type of enemey: " + enemyType);
            if (enemyHealth == 0 && gameObject.tag == "Good Zombie")
            {
                // Debug.Log("enemy heaalth is zero");
                DeathStateGood();
               

            }
            if (enemyHealth == 0 && gameObject.tag == "Bad Zombie")
            {
                DeathStateBad();

            }

            Debug.Log("enmey hit");
        }
         
        //add a collision for player --> teh zombie gets knocked back a bit

    }

    private void LoadEnemyData(string enemyName)
    {
        string path = Application.dataPath + "/Data/EnemiesText.json";

        if(File.Exists(path))
        {
            //raed json file as text and store as a sting 
            string json = File.ReadAllText(path);
            //convert json to c# object and stre the reslt 
            
            EnemyDataBase enemyStats = JsonUtility.FromJson<EnemyDataBase>(json);

            //find the correct enemy in json 
            //loop through all enimeis 

            foreach (EnemyStats enemy in enemyStats.enemiesList)
            {
                if(enemy.name == enemyName)
                {

                    enemyHealth = enemy.health;
                    speed = enemy.speed;
                    detectionRange = enemy.detectionRange;
                    attackRange = enemy.attackRange;
                    attackCoolDown = enemy.attackCoolDown;
                    attackDamage = enemy.attackDamage;
                    //Debug.Log($"Loaded enemy: {enemy.name}");

                }

            }


        }
        else
        {
            Debug.Log("enmey json file not found");

        }


    }
}
