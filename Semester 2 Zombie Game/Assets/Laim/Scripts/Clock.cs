using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Clock : MonoBehaviour
{
    public float time;
    public float timeScale = 5;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time = Mathf.Repeat(Time.time / timeScale, 24);
    }

    
}
