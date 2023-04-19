using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;

    private void Start()
    {
        // Find the flashlight component attached to this game object
        flashlight = GetComponent<Light>();
        flashlight.enabled = false;
    }

    private void Update()
    {
        // Check if the F key has been pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Toggle the flashlight on and off
            flashlight.enabled = !flashlight.enabled;
        }
    }
}
