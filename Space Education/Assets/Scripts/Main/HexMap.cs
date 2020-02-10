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

                GameObject hexGO = (GameObject)Instantiate(HexPrefab, h.Position(),
                    Quaternion.identity, this.transform);

                hexGO.name = "Hex_" + column + "_" + row;

                // Make sure the hex is aware of its place on the map
                hexGO.GetComponent<HexNew>().column = column;
                hexGO.GetComponent<HexNew>().row = row;

                // For a cleaner hierachy, parent this hex to the map
                hexGO.transform.SetParent(this.transform);


                hexGO.isStatic = true;

                MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
                mr.material = HexMaterials[UnityEngine.Random.Range(0, HexMaterials.Length)];

            }

            
        }
    }
    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
