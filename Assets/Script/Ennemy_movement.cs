using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions.Comparers;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    
    public GameObject ennemy;

    public GameObject playerObject;

    private Damage_player damagePlayerScript;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet = false;
    private float walkPointRange = 80f;

    // States
    private float sightRange = 20f;
    public bool playerInSightRange;
    public bool attackplayer;


    // Speed
    private float walkSpeed = 3.5f;
    private float runSpeed = 10;
    private float patrolSpeed;


    // Stuck detection
    private Vector3 lastPosition;
    private float lastPositionCheckTime = 0f;
    private float positionCheckInterval = 0.5f; // Interval to check the position
    private float timeAtSamePosition = 0f;
    private float maxTimeAtSamePosition = 2f; // 2 seconds threshold


    private floating_health healthBar;
    public  float maxHealth = 5, health;
    private float  dead;
    

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        lastPosition = transform.position;
        healthBar = GetComponentInChildren<floating_health>();  
    }

    public void Start()
    {
        health = maxHealth;
        damagePlayerScript = playerObject.GetComponent<Damage_player>();
        attackplayer = false;
        dead = 0;

    }

    private void Update()
    {

        if (dead == 0)
        {


            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

            if (!playerInSightRange || !CanSeePlayer())
            {
                Patroling();
            }
            if (playerInSightRange && CanSeePlayer())
            {
                ChasePlayer();
            }

            // Call the stuck detection function every update
            DetectIfStuck();
        }
        else {
            ennemy.GetComponent<Animator>().SetBool("die", true);
            ennemy.GetComponent<Animator>().SetBool("isAttacking", false);

        }

    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }

        // Randomly change speed
        if (!walkPointSet)
        {
            patrolSpeed = (Random.Range(0, 2) == 0) ? walkSpeed : runSpeed; // 50% chance to run or walk
            agent.speed = patrolSpeed;
        }
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        Vector3 randomPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, walkPointRange, NavMesh.AllAreas))
        {
            walkPoint = hit.position;
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.speed = runSpeed; 
        agent.SetDestination(player.position);

        // Look towards the player
        Vector3 directionToPlayer = player.position - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 5f);

        // Attack the player
        Attacking_Player();
    }

    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        Ray ray = new Ray(transform.position, directionToPlayer);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, sightRange))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }
        return false;
    }

    private void DetectIfStuck()
    {
        // Check the enemy's position every positionCheckInterval (0.5 seconds)
        if (Time.time >= lastPositionCheckTime + positionCheckInterval)
        {
            // If the enemy hasn't moved much, increment the timeAtSamePosition counter
            if (Vector3.Distance(transform.position, lastPosition) < 0.1f)
            {
                timeAtSamePosition += positionCheckInterval;
            }
            else
            {
                // Reset the counter if the enemy moved
                timeAtSamePosition = 0f;
            }

            // Update the last known position
            lastPosition = transform.position;
            lastPositionCheckTime = Time.time;
        }

        // If the enemy has been stuck for longer than the max time (2 seconds), find a new patrol point
        if (timeAtSamePosition >= maxTimeAtSamePosition)
        {
            walkPointSet = false;
            timeAtSamePosition = 0f; // Reset the stuck time counter
        }
    }

    void Attacking_Player(){
        if (Vector3.Distance(transform.position, player.position) <= 2f)
        {
            ennemy.GetComponent<Animator>().SetBool("isAttacking", true);
            attackplayer = true;
        }
        else 
        {
            ennemy.GetComponent<Animator>().SetBool("isAttacking", false);
            attackplayer = false;
        }
    }

    public void TakeDamage(float damage)
    {
        if (damagePlayerScript.attack)
        {
            //vérifier si le player est tourné vers l'ennemi
            
            health -= damage;
            healthBar.UpdateHealth(health, maxHealth);
            if (health <= 0)
            {

                dead = 1;
            }
        }
  

    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player collision detected");
            TakeDamage(1);
        }
    }
}
