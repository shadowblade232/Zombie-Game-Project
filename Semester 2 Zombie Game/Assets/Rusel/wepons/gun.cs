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
    private Animation anim;
    public Animator slide;
    public TextMeshProUGUI bulletct;

    public AudioSource shot;
    public AudioSource reload;


    void Start()
    {
        //ParticalSystem anim = ParticalSystem.GetComponent<MuzzleFlash>();
        
        //slide.anim = GetComponent<Animation>();
    }
    // Update is called once per frame
    void Update()
    {

         
        
        bulletct.text = ammo.ToString();
        //check if game paused
       if (Time.timeScale == 1)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                if (ammo > 0)
                {
                    shoot();
                }
            }

            if (Input.GetKey("r"))
            {
                reload.Play();
                ammo = 24;
            }
            /* if (Input.GetButtonDown("Fire1"))
            {
                if (ammo > 0)
                {
                    shoot();
                }

            }
            */
        }   

    }
    
    void shoot()
    {
        flash.Play();
        shot.Play();
        fpsCam.GetComponent<CameraShake>().shakecamera();
        ammo--;
        slide.Play("slidefire");
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            
            target targ = hit.transform.GetComponent<target>();
            if (targ != null)
            {
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
