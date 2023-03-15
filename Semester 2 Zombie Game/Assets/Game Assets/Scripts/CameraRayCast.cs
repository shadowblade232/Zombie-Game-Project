using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    public string objectLookingAt = " ";
    public GameObject gameObjectLookingAt;
    private void Update()
    {
        if(Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity))
        {        
            var obj = hit.collider.gameObject;
                if(obj.tag == "Ground Object"){    
                    Debug.Log($"looking at {obj.name}", this);
                    objectLookingAt = obj.name;
                    gameObjectLookingAt = obj;
                } else{
                    objectLookingAt = " ";
                }
        }
    }
}