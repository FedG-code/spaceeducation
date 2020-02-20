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

    /*Fed: we don't need to concern ourselves with meshs as we don't have water stuff, might be useful later on leaving it here commented
     * public Mesh MeshWater;
     * public Mesh MesFlat;
     * public Mesh MeshHill;
     * public Mesh MeshMountain;
    */
    //actually we might need 1 mesh
    public Mesh TileMesh;

    public Material MatSpace;
    public Material MatBlackHole;
    public Material MatSun;
    public Material MatAsteroid;
    public Material MatPlanet;

    public int numRows = 30;
    public int numColumns = 60;

    bool allowWrapEastWest = true;
    bool allowWrapNorthSouth = false;

    private Hex[,] hexes;
    private Dictionary<Hex, GameObject> hexToGameObjectMap;

    public Hex GetHexAt(int x, int y)
    {
        if(hexes == null)
        {
            Debug.LogError("Hexes array not yeet instantied.");
            return null;
        }

        if (allowWrapEastWest)
            x = x % numRows;
        if (allowWrapNorthSouth)
            y = y % numColumns;

        return hexes[x, y];
    }

    virtual public void GenerateMap()
    {
        hexes = new Hex[numColumns, numRows];
        hexToGameObjectMap = new Dictionary<Hex, GameObject>();

        //Generate totally random map
        for (int column = 0; column < numColumns ; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h = new Hex(column, row);
                h.Elevation = -1;
                hexes[column, row] = h;
                
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

                hexToGameObjectMap[h] = hexGO;
                hexGO.GetComponent<HexComponent>().Hex = h;
                hexGO.GetComponent<HexComponent>().HexMap = this;
                //hexGO.isStatic = true;
                hexGO.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);
                
            }

           
        }
        UpdateHexVisuals();
            //Fed: not useable for our purpose but we'll need something like this for our batching
        //StaticBatchingUtility.Combine(this.gameObject);
    }

    public void UpdateHexVisuals()
    {
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h = hexes[column, row];
                GameObject hexGO = hexToGameObjectMap[h];
                MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
                if (h.Elevation > 0)
                {
                    mr.material = MatSun;
                }
                else
                {
                    mr.material = MatSpace;
                }
                //Fed: we don't need to worry about the mesh atm we're just working with tiles that look the same
                MeshFilter mf = hexGO.GetComponentInChildren<MeshFilter>();
                mf.mesh = TileMesh;
            }
        }
    }

     public Hex[] GetHexesWithinRadiusOf(Hex centerHex, int radius)
    {
        List<Hex> results = new List<Hex>();
        for (int dx = -radius; dx < radius-1; dx++)
        {


            for (int dy = Mathf.Max(-radius+1, -dx - radius); dy < Mathf.Min(radius, -dx + radius-1); dy++)
            {
                results.Add(hexes[centerHex.Q + dx, centerHex.R + dy]);
            }

        }
        return results.ToArray();
    }
    
}
