using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float mouseSensitivity = 2.0f;
    public Transform player;

    private float verticalRotation = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 1, player.position.z);
        transform.rotation = player.rotation;

        // Handle mouse rotation
        float mouseY = -Input.GetAxis("Mouse Y");

        verticalRotation += mouseY * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
