using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float lookSensitivity = 2f;
    public float maxLookUp = 90f;
    public float maxLookDown = -90f;

    private Rigidbody rb;
    private float yRotation;
    private float xRotation;
    private Camera playerCamera;
    private bool isOnGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //check if game paused
        if (Time.timeScale == 1)
        {
            // Movement
            float moveHorizontal = Input.GetAxisRaw("Horizontal");
            float moveVertical = Input.GetAxisRaw("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical).normalized * moveSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + transform.TransformDirection(movement));

            // Jump
            if (Input.GetButtonDown("Jump") && (isOnGround))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

            // Camera Rotation
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, maxLookDown, maxLookUp);

            transform.eulerAngles = new Vector3(0f, yRotation, 0f);
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        } 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
}

