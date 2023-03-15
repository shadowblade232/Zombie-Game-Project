using UnityEngine;
using System.Collections.Generic;
using System;

public class InventoryUI : MonoBehaviour {

    // UI generation variables
    public GameObject image; // The image to use
    public int numImages = 16; // The number of images to place on the canvas
    public float margin = 5; // The margin between images   
    public int canvasWidth = 800; // The width of the canvas
    public int canvasHeight = 800; // The height of the canvas
    public float imageScale; // Scale of the Image
    public bool inventoryShown = false; // Is the inventory currently out
    float imageWidth;  
    float imageHeight;
    private int numCols, numRows; // The number of columns and rows of images to use
    private Vector3[,] imagePositions; // 2D array to store image positions

    // Inventory variables
    public GameObject testObject;
    public GameObject[] inventory;
    public int testGetPosition;

    void Start () {
        // Calculate the number of rows and columns
        numCols = (int)Math.Ceiling(Math.Sqrt(numImages));
        numRows = (int)Math.Ceiling((float)numImages / numCols);        

        // Calculate the size of each image
        imageWidth = (canvasWidth - ((numCols + 1) * margin)) / numCols;
        imageHeight = (canvasHeight - ((numRows + 1) * margin)) / numRows;

        // Calculate image positions
        imagePositions = new Vector3[numRows, numCols];
        for (int row = 0; row < numRows; row++) {
            for (int col = 0; col < numCols; col++) {
                float x = margin + col * (imageWidth + margin);
                float y = margin + row * (imageHeight + margin);
                imagePositions[row, col] = new Vector3(x, y, 0f);
            }
        }
    }

    void Update(){
        if (inventoryShown == false){
            Cursor.visible = false;
        } else{
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            testGetPosition--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            testGetPosition++;
        }

        // If e is pressed and the inventory is not being shown show it
        // If it is being shown delete it
        if (Input.GetKeyDown("e"))
        {
            if (inventoryShown == false)
            {
                inventoryShown = true;
                Debug.Log("generate inventory");
                // Place the sprites on the canvas
                for (int i = 0; i < numImages; i++) {
                    // Calculate row and column of image
                    int row = i / numCols;
                    int col = i % numCols;

                    // Create a new GameObject and add a SpriteRenderer component
                    GameObject obj = Instantiate(image, transform);
                    obj.transform.localPosition = imagePositions[row, col];
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

        if (Input.GetKeyDown("q")){
            int indexRow = (int) testGetPosition / numRows;
            int indexCol = (int) testGetPosition - (indexRow * numCols);
            GameObject testObj = Instantiate(testObject, transform);
            testObj.transform.localPosition = imagePositions[indexRow,indexCol];
            testObj.transform.localScale = Vector3.one / 4;
        }
    }

    int GetLowestOpenPosition(int[] array) {
        int lowestOpenPosition = 0;
    
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 0) // assumes 0 represents an open position
            {
                lowestOpenPosition = i;
                break;
            }
        }
    
        return lowestOpenPosition;
    }

   public void addItem(GameObject item){
       GroundObject groundObject;
       groundObject = item.GetComponent<GroundObject>();
   }
}
