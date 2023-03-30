using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
//movement variables
public float horizontalInput;
public float verticalInput;
public float speed = 5.0f;
public float mouseSensitivity = 2.0f;
public float jumpForce = 10.0f;
public Rigidbody rb;
public float maxSpeed = 10f;
public float walkSpeed = 5.0f;
public float baseSpeed = 5.0f;
public float sprintMultiplier = 10;
private float currentSpeed;
public float health = 100;
public float ammo = 25;
private bool dead = false;
public bool inventoryShown;
//public int ammo = 25;
//public int health = 100;

private GameObject canvas; // Reference to the Canvas game object

void Start()
{
    currentSpeed = baseSpeed;
    Cursor.lockState = CursorLockMode.Locked;

    // Find the Canvas game object and get its inventoryShown variable
    canvas = GameObject.Find("InventoryCanvas");
    inventoryShown = canvas.GetComponent<InventoryUI>().inventoryShown;
}

void FixedUpdate()
{
    inventoryShown = canvas.GetComponent<InventoryUI>().inventoryShown;

    if(inventoryShown == false){
        Cursor.lockState = CursorLockMode.Locked;
    } else{
        Cursor.lockState = CursorLockMode.None;
    }

    if(health <=0 && !dead)
    {
        dead = true;
        Time.timeScale = 0;
    }
    horizontalInput = Input.GetAxisRaw("Horizontal");
    verticalInput = Input.GetAxisRaw("Vertical");
    float mouseX = Input.GetAxis("Mouse X");

    // Handle player movement
    Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);
    Quaternion rotation = Quaternion.Euler(0, transform.eulerAngles.y + mouseX * mouseSensitivity, 0);
    movement = rotation * movement;

    if (Input.GetKey(KeyCode.LeftShift))
    {
        currentSpeed = baseSpeed * sprintMultiplier;
    }
    else
    {
        currentSpeed = baseSpeed;
    }

    transform.position += movement * currentSpeed * Time.deltaTime;

    // Handle player rotation
    transform.Rotate(Vector3.up, mouseX * mouseSensitivity);

    if (Input.GetKeyDown("space") && IsGrounded())
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}

bool IsGrounded()
{        
    // Check if the player is grounded by casting a ray from their current position downwards
    if (gameObject.transform.position.y < 3)
    {
        return true;  
    }
    return false;
}

public void takeDamage(float amount)
{
    health -= amount;
}
}