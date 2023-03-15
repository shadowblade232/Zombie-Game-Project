
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

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("fire") && Time.time >= nextFire)
        {
            nextFire = Time.time + 1f / fireRate;
            shoot();
        }

        if (input.GetButtonDown("fire"))
        {
            shoot();
        }


    }
    
    void shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.postition, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            target targ = hit.transform.GetComponent<target>();
            if (targ != null)
            {
                target.damage(damage); 
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactFRC);
            }

        }

        

    }
}
