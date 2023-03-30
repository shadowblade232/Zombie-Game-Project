using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerUI : MonoBehaviour
{
    private float health;
    private float ammo;
    private float kills;
    public GameObject player;
    private PlayerController playerController;
    //public TextMeshProUGUI healthText;
    //public TextMeshProUGUI ammoText;
    //public TextMeshProUGUI killsText;
    public TextMeshProUGUI lookingAt;


    // Start is called before the first frame update
    void FixedStart()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //ammo = playerController.ammo;
        //health = playerController.health;
        //healthText.text = health.ToString("0");
        //ammoText.text = ammo.ToString("0");
        //killsText.text = kills.ToString("0");
        lookingAt.text = "test";
    }
}
