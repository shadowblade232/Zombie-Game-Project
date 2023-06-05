using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class zombiehit : MonoBehaviour
{
    public GameObject player;
        private GameObject zombie;
    public int playerh = 100;
    public TextMeshProUGUI health;
    // public scene thescene;

    void Start()
    {
        //player = GameObject.Find("player");
        // playerre = GetComponent<playerre>();
        zombie = GameObject.FindWithTag("zombie");

    }
    // Start is called before the first frame update
   void Update()
    {
        /*  if ((zombie.transform.position - player.transform.position).sqrMagnitude < 1.2 * 1.2)
          {

              hit(2);
          }
        */
        health.text = "player health= "+ playerh.ToString() + "/100";
        if (playerh <= 0)
        {
            SceneManager.LoadScene("Master Scene 1");
        }
    }
    
    // Start is called before the first frame update
    public void hit(int damager)
    {

        playerh = playerh - damager;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.name == "Zombie3(Clone)")
        {
            Debug.Log("contact");
            hit(10);
        }
    }
    
}
