using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    //basic variables
    private Transform target; // the target the zombie is currently chasing
    private GameObject player; // the player object
    private NavMeshAgent agent; // the NavMeshAgent component
    private Vector3 targetPosition; // the target position for the zombie to move to
    private Rigidbody rb; // the Rigidbody component
    private PlayerInfo playerUi; // the PlayerUI component
    private Vector3 spawnPosition; // the initial position of the zombie
    public float spawnDistance; // the distance from the spawn position to start chasing the player
    //vision variables
    public int numRays; // the number of rays to cast for vision detection
    public float range; // the range of the rays for vision detection
    public float coneAngle; // the angle of the cone of vision
    //random move variables
    public float minWaitTime = 1.0f; // the minimum time to wait before moving to a new random position
    public float maxWaitTime = 5.0f; // the maximum time to wait before moving to a new random position
    public float minMoveDistance = 1.0f; // the minimum distance to move to a new random position
    public float maxMoveDistance = 10.0f; // the maximum distance to move to a new random position
    //memory variables
    public float memoryDuration; // the duration of the memory of the player's position
    public float memoryTimer = 0.0f; // the timer for the memory
    //zombie variables
    public float health; // the health of the zombie
    public Material damagedMaterial; // the material to use when the zombie is damaged
    public float damageTimer; // the timer for the damaged material
    private Material starterMaterial; // the starting material of the zombie
    private float timeSinceAttack = 1; // the time since the last attack
    public float targetDistance; // the distance to the target
    private bool isDead = false; // a flag to indicate if the zombie is dead
    private float timeDead; // how long the zombie has been dead

    //particles to play when it dies
    public GameObject dieParticles;
   

    void Start()
    {
        player = GameObject.FindWithTag("player"); // find the player object by tag
        agent = GetComponent<NavMeshAgent>(); // get the NavMeshAgent component
        starterMaterial = gameObject.GetComponent<MeshRenderer>().material; // get the starting material of the zombie
        rb = gameObject.GetComponent<Rigidbody>(); // get the Rigidbody component
        target = player.transform; // set the target to the player object
        playerUi = GetComponent<PlayerInfo>(); // get the PlayerUI component
        spawnPosition = gameObject.transform.position; // set the spawn position to the initial position of the zombie
    }

    void Update()
    {
        // Calculate the distance between the enemy and the target
        targetDistance = Vector3.Distance(gameObject.transform.position, target.position);
    
        // If the enemy is too close to the target do not do anything
        if(!(targetDistance > spawnDistance)){
            return;
        }
        // If the enemy is dead and they are a certain distance from the player come back to life
        if(isDead && targetDistance > spawnDistance && timeDead < 100){
            isDead = false;
            gameObject.transform.position = spawnPosition;
        }
        // If the enemy is close enough to attack the player, increase time since the last attack and attack if enough time has passed
        if (Vector3.Distance(gameObject.transform.position, target.position) < 1.5)
        {
            timeSinceAttack += Time.deltaTime;
            if (timeSinceAttack >= 1) {
                // Get the player controller component and damage the player
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.takeDamage(5);
                timeSinceAttack = 0;
            }
        }
    
        // If the enemy should chase the player, set the agent's destination to the player's current position
        if (shouldChase())
        {
            agent.SetDestination(target.position);
        }
        else // If the enemy should not chase the player, choose a new random target position within the nav mesh and set the agent's destination to that position
        {
            targetPosition = RandomNavmeshLocation(minMoveDistance, maxMoveDistance);
            agent.SetDestination(targetPosition);
        }    
        // If the enemy's material is not the original material, gradually change it back to the original material
        if(gameObject.GetComponent<MeshRenderer>().material != starterMaterial)
        {
            changeMaterial(.5f, GetComponent<Renderer>(), starterMaterial);
        }    
        // If the enemy's health is zero or below, destroy the enemy
        if(health <= 0)
        {
            Instantiate(dieParticles, this.transform);
            gun gunController = player.GetComponent<gun>();
            gunController.kills++;
            isDead = true;
        }
    }
    
    // Calculate the distance from the enemy to the closest object in front of it
    private float raycastDistance() {
        float distance;
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, forward, out hit, Mathf.Infinity) && !hit.transform.CompareTag("Player"))
        {
            distance = hit.distance;
        }
        else
        {
            distance = 0;
        }
        return distance;
    }
    
    // Determine whether the enemy should chase the player based on whether the player is within vision range or in memory
    public bool shouldChase()
    {
        bool shouldChase = false;
    
        // Check if the target is within the vision range and line of sight is clear
        if (inVision(numRays, range, coneAngle))
        {
            shouldChase = true;
        }
    
        // Check if the target is in memory
        if (inMemory())
        {
            shouldChase = true;
        }
    
        // Check if the enemy is close to its target position
        if(Vector3.Distance(gameObject.transform.position, targetPosition) < 5)
        {
            shouldChase = true;
        }
    
        return shouldChase;
    }
    
    // Damage the enemy and change its material to the damaged material for a short time
    public void takeDamage(float amount)
    {
        health -= amount;
    
        // Get the renderer component
        Renderer renderer = GetComponent<Renderer>();
    
        // Store the current material
        Material originalMaterial = renderer.material;
    
        // Change the material to the new material
        renderer.material = damagedMaterial;
    
        damageTimer = 0;
    }

    // When the zombie takes damage it changes to another material to indicate damage
    private void changeMaterial(float seconds, Renderer renderer, Material originalMaterial)
    {
        damageTimer += Time.deltaTime;
        if(damageTimer >= .5f)
        {
            // Change the material back to the original material
            renderer.material = originalMaterial;
            damageTimer = 0;
        }
    }

    // Check if the enemy can see the player
    private bool inVision(int numRays, float range, float coneAngle)
    {
        bool inVision = false;

        for (int i = 0; i < numRays; i++)
        {
            // Calculate the angle of the current ray
            float angle = (-coneAngle / 2) + (((float)i / (numRays - 1)) * coneAngle);

            // Rotate the ray around the player
            Vector3 direction = Quaternion.AngleAxis(angle, transform.up) * transform.forward;

            // Create a random rotation around the direction
            Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(15, 105));
            direction = randomRotation * direction;

            // Cast the ray and visualize it
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, range))
            {
                if (hit.transform == target)
                {
                    // The target is within the vision range and the line of sight is clear, so set the flag and exit the loop
                    inVision = true;
                    break;
                }
            }
        }

        return inVision;
    }

    // Comes up with a random location within a radius
    private Vector3 RandomNavmeshLocation(float minDistance, float maxDistance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, maxDistance, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    
    // Check if the player is within the memory and if they are within vision reset memory
    private bool inMemory()
    {
        bool memory = false;
        memoryTimer += Time.deltaTime;
        if (inVision(numRays, range, coneAngle))
        {
            memoryTimer = 0;
        }

        if(memoryTimer < memoryDuration)
        {
            memory = true;
        }
        return memory;
    }
}