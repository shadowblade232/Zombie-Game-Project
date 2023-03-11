using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject zombie;
    public Transform player;
    private PlayerController playerController;
    public int maxZombies = 1;
    public float spawnRadius = 10f;
    public int length;
    public float minSpawn;
    public float maxSpawn;
    public float spawnRate;
    private float spawnTimer;
    public float levelTimer;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }
    void Update()
    {
        levelTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        maxZombies += (int)(levelTimer / 10);

        if (playerController.health <=0)
        {
            maxZombies = 1;
        }

        gameObject.transform.position = player.position;
        length = GameObject.FindGameObjectsWithTag("Zombie").Length;
        if (GameObject.FindGameObjectsWithTag("Zombie").Length < maxZombies)
        {
            if (spawnTimer >= spawnRate) {
                Instantiate(zombie, RandomNavmeshLocation(minSpawn, maxSpawn), Quaternion.identity);
                spawnTimer = 0;
            }
        }
        else
        {
            GameObject farthestZombie = null;
            float farthestDistance = 0f;

            foreach (GameObject zombie in GameObject.FindGameObjectsWithTag("Zombie"))
            {
                float distance = Vector3.Distance(zombie.transform.position, player.position);
                if (distance > farthestDistance)
                {
                    farthestDistance = distance;
                    farthestZombie = zombie;
                }
            }

            Destroy(farthestZombie);
        }
    }

    private Vector3 RandomNavmeshLocation(float minDistance, float maxDistance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += transform.position;
        UnityEngine.AI.NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, maxDistance, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
