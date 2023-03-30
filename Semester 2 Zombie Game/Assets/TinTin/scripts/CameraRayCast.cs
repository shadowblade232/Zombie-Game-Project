using UnityEngine;

public class CameraRayCast : MonoBehaviour
{
    public float overlapRadius = 5.0f;
    public GameObject lookingAt;

    private void Update()
    {
        lookingAt = DetectObject();
    }

    public GameObject DetectObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, overlapRadius);

        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            float distanceToCollider = Vector3.Distance(transform.position, collider.transform.position);
            if (distanceToCollider < closestDistance)
            {
                closestDistance = distanceToCollider;
                closestObject = collider.gameObject;
            }
        }
        Debug.Log("Looking at:" + closestObject.ToString());
        return closestObject;
    }
}
