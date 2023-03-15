
using UnityEngine;

public class target : MonoBehaviour
{
    public float health = 50f;

    public void damage(float amt)
    {
        health -= amt;
        if (health <= 0f)
        {
            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }

}
