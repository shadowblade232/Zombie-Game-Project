using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    //damage gun inflicts
    public float damage = 50f;
    //range of shot
    public float range = 100f;
    public float impactFRC = 30f;
    public float fireRate = 20f;
    public Camera fpsCam;
    private float nextFire = 0f;
    public 
    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1") && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            shoot();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
        

    }
    
    void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            
            target targ = hit.transform.GetComponent<target>();
            if (targ != null)
            {
                targ.damage(damage); 
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactFRC);
            }

        }

        Debug.Log("pew");

    }
}
