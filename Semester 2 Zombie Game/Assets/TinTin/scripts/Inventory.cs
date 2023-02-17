using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour
{

    // A list of images to use for the inventory items
    public int inventorySpaces = 5;
    public Canvas inventoryCanvas;

    public List<GameObject> inventoryItemPrefab;
    private List<GameObject> lastInventoryItemPrefab;

    public int mouse;
    public GameObject selector;

    void Start()
    {
        // Initialize the inventoryItemPrefab and lastInventoryItemPrefab lists
        List<GameObject> inventoryItemPrefab = new List<GameObject>();
        lastInventoryItemPrefab = new List<GameObject>(inventoryItemPrefab);
    }

    void Update()
    {
        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheelInput > 0 && mouse < inventoryItemPrefab.Count - 1)
        {
            mouse++;
        }
        else if (mouseWheelInput < 0 && mouse > 0)
        {
            mouse--;
        }

        selector.transform.position = new Vector3(105, (940 - (mouse * 200)), 0);

        // Check if the inventoryItemPrefab list has changed since the last frame
        bool hasChanged = !inventoryItemPrefab.SequenceEqual(lastInventoryItemPrefab);
        if (hasChanged)
        {
            // If the list has changed, refresh the list by calling the refreshList method
            refreshList(inventoryItemPrefab, inventoryCanvas);
        }

        // Make a copy of the inventoryItemPrefab list to use as a reference for the next frame
        lastInventoryItemPrefab = new List<GameObject>(inventoryItemPrefab);
    }

    // The refreshList method is called to refresh the list of inventory items on the screen
    static void refreshList(List<GameObject> inventoryItemPrefab, Canvas inventoryCanvas)
    {
        // First, destroy all of the existing items on the screen, except for those with the "DoNotDestroy" tag
        foreach (Transform child in inventoryCanvas.transform)
        {
            if (child.gameObject.tag != "DoNotDestroy")
            {
                Destroy(child.gameObject);
            }
        }

        // Then, add all of the items in the list to the screen
        for (int i = 0; i < inventoryItemPrefab.Count; i++)
        {
            int y = 400 - (i * 200);
            GameObject creation = Instantiate(inventoryItemPrefab[i], new Vector3(-850, y, 0), Quaternion.identity);
            creation.transform.SetParent(inventoryCanvas.transform, false);
        }
    }

}