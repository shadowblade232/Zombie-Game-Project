using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    //private float health;
    //private float ammo;
    //private float kills;
    public GameObject player;
    private PlayerController playerController;
    public TextMeshProUGUI lookingAt;
    public GameObject camera;
    public CameraRayCast cameraRayCast;
    public string testLooking;
    public GameObject canvas;
    public InventoryUI inventoryUI;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        cameraRayCast = camera.GetComponent<CameraRayCast>();
        inventoryUI = canvas.GetComponent<InventoryUI>();
    }

    // Update is called once per frame
    void Update()
    {
        testLooking = cameraRayCast.objectLookingAt;
        lookingAt.text = cameraRayCast.objectLookingAt;

        if(Input.GetKeyDown("r")){
            inventoryUI.addItem(cameraRayCast.gameObjectLookingAt);
        }
    }
}
