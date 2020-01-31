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

    public void GenerateMap()
    {
        for (int column = 0; column <3; column++)
        {
            for (int row = 0; row < 3; row++)
            {
                Hex h = new Hex(column, row);

                GameObject hexGO = (GameObject)Instantiate(HexPrefab, h.Position(),
                    Quaternion.identity, this.transform);

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
