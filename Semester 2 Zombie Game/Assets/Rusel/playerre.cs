using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerre : MonoBehaviour
{
    public int playerh = 100;
    // Start is called before the first frame update
    public void hit()
    {
        Debug.Log("zhit");
        playerh = playerh - 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerh <= 0)
        {
            SceneManager.LoadScene("Master Scene");
        }
    }
}
