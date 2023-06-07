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
    public GameObject deathMenuUI;
    public float elapsedTime;
    public int kills;
    public int score;
    public TextMeshProUGUI scoreCount;
    // public scene thescene;

    void Start()
    {
        //player = GameObject.Find("player");
        // playerre = GetComponent<playerre>();
        zombie = GameObject.FindWithTag("zombie");
        deathMenuUI.SetActive(false);

    }
    // Start is called before the first frame update
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (kills > 0)
        {
            score = (int)(elapsedTime / Mathf.Pow(elapsedTime, .5f) * kills);
        }
        else
        {
            score = (int)(elapsedTime / Mathf.Pow(elapsedTime, .5f) * kills + 1);
        }

        scoreCount.text = score.ToString();
        /*  if ((zombie.transform.position - player.transform.position).sqrMagnitude < 1.2 * 1.2)
          {

              hit(2);
          }
        */
        health.text = "player health= "+ playerh.ToString() + "/100";
        if (playerh <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0f; // Freeze the game
            deathMenuUI.SetActive(true);
        }
    }

    public void restart()
    {
        deathMenuUI.SetActive(false);
        SceneManager.LoadScene("QMasterScene");
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
