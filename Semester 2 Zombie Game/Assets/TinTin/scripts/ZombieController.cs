using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    //basic variables
    private Transform target;
    private GameObject player;
    NavMeshAgent agent;
    private Vector3 targetPosition;
    private Rigidbody rb;
    private PlayerUI playerUi;
    //vision variables
    public int numRays;
    public float range;
    public float coneAngle;
    //random move variables
    public float minWaitTime = 1.0f;
    public float maxWaitTime = 5.0f;
    public float minMoveDistance = 1.0f;
    public float maxMoveDistance = 10.0f;
    //memory variables
    public float memoryDuration;
    public float memoryTimer = 0.0f;
    //zombie variables
    public float health;
    public Material damagedMaterial;
    public float damageTimer;
    private Material starterMaterial;
    private float timeSinceAttack = 1;
    public float targetDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        starterMaterial = gameObject.GetComponent<MeshRenderer>().material;
        rb = gameObject.GetComponent<Rigidbody>();
        target = player.transform;
        playerUi = GetComponent<PlayerUI>();
    }

    void Update()
    {
        targetDistance = Vector3.Distance(gameObject.transform.position, target.position);
        if (Vector3.Distance(gameObject.transform.position, target.position) < 1.5)
        {
            timeSinceAttack += Time.deltaTime;
            if (timeSinceAttack >= 1) {
                PlayerController playerController = player.GetComponent<PlayerController>();
                playerController.takeDamage(5);
                timeSinceAttack = 0;
            }
        }
        if (shouldChase())
        {
            // Set the agent's destination to the player's current position
            agent.SetDestination(target.position);                       
        }
        else
        {
            // Choose a new random target position within the nav mesh
            targetPosition = RandomNavmeshLocation(minMoveDistance, maxMoveDistance);

            // Set the agent's destination to the target position
            agent.SetDestination(targetPosition);
        }

        if(raycastDistance() < 5)
        {
            rb.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }

        if(gameObject.GetComponent<MeshRenderer>().material != starterMaterial)
        {
            changeMaterial(.5f, GetComponent<Renderer>(), starterMaterial);
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

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
        if(Vector3.Distance(gameObject.transform.position, targetPosition) < 5)
        {
            shouldChase = true;
        }

        return shouldChase;
    }

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