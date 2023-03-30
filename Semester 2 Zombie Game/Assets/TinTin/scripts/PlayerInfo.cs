using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    private float health;
    //private float ammo;
    //private float kills;
    public TextMeshProUGUI healthText;
    public GameObject player;
    private PlayerController playerController;
    public TextMeshProUGUI lookingAt;
    public GameObject camera;
    public CameraRayCast cameraRayCast;
    public string testLooking;
    public GameObject canvas;
    public InventoryUI inventoryUI;

    private float verticalScale;
    private float horizontalScale;
    private float averageScale;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        cameraRayCast = camera.GetComponent<CameraRayCast>();
        inventoryUI = canvas.GetComponent<InventoryUI>();
        health = playerController.health;
    }

    // Update is called once per frame
    void Update()
    {
        verticalScale = ((float)Screen.height / 1080);
        horizontalScale = ((float)Screen.width / 1920);
        averageScale = (float)(verticalScale + horizontalScale) / 2;
        health = playerController.health;
        healthText.text = health.ToString("0");
        healthText.transform.localPosition = new Vector2(850 * averageScale, 500 * averageScale);
        healthText.fontSize = 94 * averageScale;

     

        if(Input.GetKeyDown("r")){
            inventoryUI.addItem(cameraRayCast.lookingAt);
        }
    }
}
