using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCrafter : MonoBehaviour
{
    public GameObject output;
    public List<GameObject> items;

    public List<GameObject> inventoryItemPrefab;
    private GameObject canvas;
    private Inventory inventory;

    public TextMesh text;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        inventory = canvas.GetComponent<Inventory>();
        inventoryItemPrefab = inventory.inventoryItemPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        inventoryItemPrefab = inventory.inventoryItemPrefab;
    }

    public bool existsInInventory(List<GameObject> items, List<GameObject> inventoryItemPrefab)
    {
        bool containsAll = true;
        Debug.Log("crafting");
        for (int i = 0; i < items.Count; i++)
        {
            if (!(inventoryItemPrefab.Contains(items[i])))
            {
                containsAll = false;
            }
        }
        return containsAll;
    }

    void OnMouseDown()
    {
        Debug.Log("kettle clicked");
        if (existsInInventory(items, inventoryItemPrefab))
        {
            // Remove all elements from listA from listB
            for (int i = 0; i < items.Count; i++)
            {
                inventoryItemPrefab.Remove(items[i]);
            }

            // Add a new element to listB
            inventoryItemPrefab.Add(output);
        }
    }

}
