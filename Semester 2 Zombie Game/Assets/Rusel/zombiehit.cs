using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombiehit : MonoBehaviour
{
    
    // Start is called before the first frame update
   
    
    void OnCollisionEnter(Collision collision)
    {
        playerre code = gameObject.FindWithTag.("player").GetComponent<playerre>();

        if (collision.gameObject.tag == "player")
        {
            code.hit();
        }
    }
}
