using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class HexMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){
        GenerateMap();   
    }

    public GameObject HexPrefab;

    public Material[] HexMaterials;

    public int numRows = 20;
    public int numColumns = 20;


    public void GenerateMap()
    {
        for (int column = 0; column < numColumns ; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h = new Hex(column, row);

                //Fed: omitted from tutorial 2 video, comments pointed me to this. This definiton of pos is what makes the square map
                Vector3 pos = h.PositionFromCamera(
                Camera.main.transform.position,
                numRows,
                numColumns
            );

GameObject hexGO = (GameObject)Instantiate(
                    HexPrefab,
                    //Fed: same omission from tutorial
                    pos,
                    Quaternion.identity,
                    this.transform
            );

                //hexGO.name = "Hex_" + column + "_" + row;

                // Make sure the hex is aware of its place on the map
                //hexGO.GetComponent<HexNew>().column = column;
                //hexGO.GetComponent<HexNew>().row = row;

                // For a cleaner hierachy, parent this hex to the map
                //hexGO.transform.SetParent(this.transform);

                hexGO.GetComponent<HexComponent>().Hex = h;
                hexGO.GetComponent<HexComponent>().HexMap = this;
                //hexGO.isStatic = true;

                MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
                mr.material = HexMaterials[UnityEngine.Random.Range(0, HexMaterials.Length)];

            }

           
        }
            //Fed: not useable for our purpose but we'll need something like this for our batching
        //StaticBatchingUtility.Combine(this.gameObject);
    }
    
}
