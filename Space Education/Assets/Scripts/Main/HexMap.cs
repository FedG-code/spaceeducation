using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class HexMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    public GameObject HexPrefab;

    // Samuel: Current player / unit. This has been manually sected, by dragging
    // the unit gameobject into this field
    public GameObject SelectedUnit;

    /*
     * Fed: we don't need to concern ourselves with meshs as we don't have water
     * stuff, might be useful later on leaving it here commented
     * public Mesh MeshWater;
     * public Mesh MesFlat;
     * public Mesh MeshHill;
     * public Mesh MeshMountain;
    */
    //actually we might need 1 mesh
    public Mesh TileMesh;

    public Material MatSpace;
    public Material MatBlackHole;
    public Material MatStar;
    public Material MatAsteroid;
    public Material MatPlanet;

    public Material MatEvent;

    public Material MatBadPlanet;

    public int numRows = 40;
    public int numColumns = 40;

    public bool allowWrapEastWest = true;
    public bool allowWrapNorthSouth = false;

    public HexTileBase[,] hexes { get; set; }

    private Dictionary<HexTileBase, GameObject> hexToGameObjectMap;

    public HexTileBase GetHexAt(int x, int y)
    {
        if (hexes == null)
        {
            Debug.LogError("Hexes array not yet instantied.");
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
        hexes = new HexTileBase[numColumns, numRows];
        hexToGameObjectMap = new Dictionary<HexTileBase, GameObject>();
        //Generate totally random map
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                //Fed: omitted from tutorial 2 video, comments pointed me to this. This definiton of pos is what makes the square map
                SpaceTile h = new SpaceTile(this, column, row);
                //h.tiletype = -1;
                hexes[column, row] = h;

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

                // Samuel: Get clickable tile component from hex prefab
                ClickableTile ct = hexGO.GetComponent<ClickableTile>();
                ct.tileQ = column;
                ct.tileR = row;

                h.GameObject = hexGO;
                MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
                mr.material = MatSpace;
                hexToGameObjectMap[h] = hexGO;
                hexGO.GetComponent<HexComponent>().Hex = h;
                hexGO.GetComponent<HexComponent>().HexMap = this;
                //hexGO.isStatic = true;
                hexGO.GetComponentInChildren<TextMesh>().text = string.Format("{0},{1}", column, row);

            }
        }

       // UpdateHexVisuals();

        //Fed: not useable for our purpose but we'll need something like this for our batching
        StaticBatchingUtility.Combine(this.gameObject);
    }

    //public void UpdateHexVisuals()
    //{
    //    for (int column = 0; column < numColumns; column++)
    //    {
    //        for (int row = 0; row < numRows; row++)
    //        {
    //            HexTileBase h = hexes[column, row];
    //            GameObject hexGO = hexToGameObjectMap[h];

    //            MeshRenderer mr = hexGO.GetComponentInChildren<MeshRenderer>();
    //            //ideally will convert the value for h.Elevation to a string soon but atm it's just connected to different values
    //            //we have a switch instead of if statement because wee don't need a range in our values
    //            switch (h.tiletype)
    //            {
    //                case HexTileBase.TILE_TYPE.STAR:
    //                    mr.material = MatStar;
    //                    break;

    //                case HexTileBase.TILE_TYPE.PLANET:
    //                    mr.material = MatPlanet;
    //                    break;

    //                case HexTileBase.TILE_TYPE.ASTEROID:
    //                    mr.material = MatAsteroid;
    //                    break;

    //                case HexTileBase.TILE_TYPE.EVENT:
    //                    mr.material = MatEvent;
    //                    break;

    //                case HexTileBase.TILE_TYPE.BLACKHOLE:
    //                    mr.material = MatBlackHole;
    //                    break;

    //                case HexTileBase.TILE_TYPE.BADPLANET:
    //                    mr.material = MatBadPlanet;
    //                    break;

    //                default:
    //                    mr.material = MatSpace;
    //                    break;
    //            }

    //            //Fed: we don't need to worry about the mesh atm we're just working with tiles that look the same
    //            MeshFilter mf = hexGO.GetComponentInChildren<MeshFilter>();
    //            mf.mesh = TileMesh;
    //        }
    //    }
    //}

    public List<HexTileBase> GetHexesWithinRadiusOf(HexTileBase centerHex, int range)
    {
        List<HexTileBase> results = new List<HexTileBase>();
        for (int dx = -range; dx < range - 1; dx++)
        {
            for (int dy = Mathf.Max(-range + 1, -dx - range); dy < Mathf.Min(range, -dx + range - 1); dy++)
            {
                results.Add(hexes[centerHex.Q + dx, centerHex.R + dy]);
            }

        }
        return results;
    }

    public void MoveSelectedUnitTo(int Q, int R) {
    /**
     * Author: Samuel Overington
     * Function to connect to component ClickableTile.cs
     * NB Currently unit (player), is permanently selected
     * (see GameObject SelectedUnit in the head of this file)
     **/

    }

}
