using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{
    public GameObject hexPrefab;
    int width = 5;
    int height = 5;

    float xOffset = 0.875f; //double 0.434
    float zoffset = 0.758f;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++){
                float xPos = x*xOffset;
                // Check for odd row
                if (y % 2 == 1)
                {
                    // if odd row then offset by the given amount
                    xPos +=  xOffset/2;
                }


                GameObject hex_go = (GameObject)Instantiate(hexPrefab, new Vector3(xPos, 0, y*zoffset), Quaternion.identity);
                hex_go.name = "Hex_" + x + "_" + y;

                hex_go.transform.SetParent(this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
