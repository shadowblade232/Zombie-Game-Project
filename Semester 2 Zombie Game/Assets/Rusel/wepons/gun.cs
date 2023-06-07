using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    public ParticleSystem flash;
    public int ammo = 24;
   // public int score = 0;
   // public TextMeshProUGUI scoreCount;
    private Animation anim;
    public Animator slide;
    public TextMeshProUGUI bulletct;
    public float elapsedTime = 0;
    //public int hits = 0;

    public AudioSource shot;
    public AudioSource reload;


    void Start()
    {
        elapsedTime = 0;
        //ParticalSystem anim = ParticalSystem.GetComponent<MuzzleFlash>();
        
        //slide.anim = GetComponent<Animation>();
    }
    // Update is called once per frame
    void Update()
    {
        bulletct.text = ammo.ToString();
        //scoreCount.text = ((int)hits / 2).ToString();
        
        //check if game paused
       if (Time.timeScale == 1)
        {
            /* if (Input.GetButton("Fire1") && Time.time >= nextFire)
             { 
            Debug.Log("pew");
                 nextFire = Time.time + 1f / fireRate;
                 if (ammo > 0)
                 {
                     shoot();
                 }
             } */

           if (Input.GetKey("r"))
            {
                reload.Play();
                ammo = 24;
            }

             if (Input.GetButtonDown("Fire1"))
            {
                if (ammo > 0)
                {
                    shoot();
                }

            }
           
            
        }   

    }
    
    void shoot()
    {
        flash.Play();
        shot.Play();
        fpsCam.GetComponent<CameraShake>().shakecamera();
        ammo--;
        slide.Play("slidefire");
        Ray ray = new Ray(fpsCam.transform.position, fpsCam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);

        RaycastHit hit;
        float radius = 1;
        if (Physics.Raycast(ray, out hit, range))
       // if (Physics.SphereCast(ray, radius, out hit, range))
        {

            target targ = hit.transform.GetComponent<target>();
            if (targ != null)
            {
               // hits++;
                targ.damage(damage);

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactFRC);
                }
            }
        }
        

        // Debug.Log("pew");

    }
}
