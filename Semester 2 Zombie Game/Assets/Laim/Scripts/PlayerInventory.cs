using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int alcoholCount;
    private int ragCount;

    private void Update()
    {
        // when "m" is pressed, check if player has rags and alcohol
        if ((Input.GetKeyDown(KeyCode.M)) && alcoholCount != 0 && ragCount != 0)
        {
            //remove rags and alcohol
            alcoholCount--;
            ragCount--;

            //craft medkit
            Debug.Log("medkit crafted");
        }

        // when "L" is pressed, check if player has rags and alcohol
        if ((Input.GetKeyDown(KeyCode.L)) && alcoholCount != 0 && ragCount != 0)
        {
            //remove rags and alcohol
            alcoholCount--;
            ragCount--;

            //craft molotov
            Debug.Log("molotov crafted");
        }

        // dev tool to add alcohol and rags to inventory
        if (Input.GetKeyDown(KeyCode.P))
        {
            alcoholCount++;
            ragCount++;

            Debug.Log("alcoholCount++ ragCount++");
        }
    }
}
