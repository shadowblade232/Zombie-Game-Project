using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public int height;
    public int width;
    public GameObject uiImagePrefab; // the prefab for the UI image you want to create
    private float imageSize; // the actual size of each image
    public float imageSizeSetting; // the outward facing image sizer
    public float spacing; // the spacing between each image
    private bool inventoryShown;
    public float resolution;

    void Start()
    {
        Debug.Log(Screen.currentResolution);
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            if (inventoryShown == false)
            {
                inventoryShown = true;
                Debug.Log("generate inventory");
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        // calculate the position of the new image based on its row and column
                        float x = j * (imageSize + spacing) - ((width - 1) * (imageSize + spacing) / 2f);
                        float y = i * (imageSize + spacing) - ((height - 1) * (imageSize + spacing) / 2f);

                        // instantiate a new UI image prefab and position it
                        GameObject newImage = Instantiate(uiImagePrefab, transform);
                        newImage.transform.localPosition = new Vector3(x, y, 0);
                        newImage.transform.localScale = Vector3.one * imageSize;
                    }
                }
            }
            else
            {
                inventoryShown = false;
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Inventory UI");
                foreach (GameObject obj in objects)
                {
                    Destroy(obj);
                }
            }
        }
    }
}
