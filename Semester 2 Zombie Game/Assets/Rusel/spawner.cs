using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnPointMarker;
    public spawnIntake[] spawnloc;
    public bool already = false;
    public int numOfSpawn = 0; 
   
    
    // Start is called before the first frame update
    void Start()
    {
        spawnIntake[] spawnloc = new spawnIntake[0];

        spawngo(2);
    }

    // Update is called once per frame
    void Update()
    {
       //spawnIntake spawnloc = gameObject.GetComponent<spawnIntake>();
        if (already == false && (spawnPointMarker.transform.position - player.transform.position).sqrMagnitude < 20*20)
        {
            Debug.Log("spawn");

            spawngo(3);
            already = true;


            // Instantiate(zombie, new Vector3(location.transform.position));
        }
        if ((spawnPointMarker.transform.position - player.transform.position).sqrMagnitude > 20 * 20)
        {
            already = false;
        }




    }

    public void spawngo(int number)
    {
        for (int i = 0; i < numOfSpawn; i++)
        {
            spawnloc[i].spawnin(number);

        }

    }
}
