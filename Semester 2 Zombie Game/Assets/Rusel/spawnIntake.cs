using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnIntake : MonoBehaviour
{
    public GameObject zombie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawnin()
    {
        Debug.Log("pp");
        Instantiate(zombie, transform.position, transform.rotation);
    }
}
