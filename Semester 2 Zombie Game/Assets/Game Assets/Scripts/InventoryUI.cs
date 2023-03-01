using UnityEngine;
using System.Collections.Generic;
using System;

public class InventoryUI : MonoBehaviour {

    public GameObject image; // The image to use
    public int numImages = 16; // The number of images to place on the canvas
    public float margin = 5; // The margin between images
    public int canvasWidth = 800; // The width of the canvas
    public int canvasHeight = 800; // The height of the canvas
    public float imageScale; // Scale of the Image
    public bool inventoryShown;
    float imageWidth ;
    float imageHeight;

    private List<Sprite> sprites = new List<Sprite>(); // The list of sprites to use
    private int numCols, numRows; // The number of columns and rows of images to use

    // Use this for initialization
    void Start () {
        // Calculate the number of rows and columns
        numCols = (int)Math.Ceiling(Math.Sqrt(numImages));
        numRows = (int)Math.Ceiling((float)numImages / numCols);

        // Calculate the size of each image
        imageWidth = (canvasWidth - ((numCols + 1) * margin)) / numCols;
        imageHeight = (canvasHeight - ((numRows + 1) * margin)) / numRows;

        

        
    }

    void Update(){
        if (Input.GetKeyDown("e"))
        {
            if (inventoryShown == false)
            {
                inventoryShown = true;
                Debug.Log("generate inventory");
                // Place the sprites on the canvas
                for (int i = 0; i < numImages; i++) {
                    // Calculate position of image
                    int row = i / numCols;
                    int col = i % numCols;
                    float x = margin + col * (imageWidth + margin);
                    float y = margin + row * (imageHeight + margin);

                    // Create a new GameObject and add a SpriteRenderer component
                    GameObject obj = Instantiate(image, transform);
                    obj.transform.localPosition = new Vector3(x, y, 0f);
                    obj.transform.localScale = Vector3.one * imageScale;
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
