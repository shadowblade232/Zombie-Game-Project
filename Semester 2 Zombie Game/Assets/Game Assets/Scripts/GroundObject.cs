using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundObject : MonoBehaviour
{
    public float spinSpeed = 5f;
    public Color highlightColor = Color.yellow;
    private bool isHighlighted = false;
    public GameObject sprite;

    //public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        // Remove physics properties
        var rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HitByRay()
    {
        // This method will be called when the object is hit by a raycast
        Debug.Log("Object was hit by raycast!");
    }

    void OnMouseOver(){
        Debug.Log("Mouse over");
    }
}
