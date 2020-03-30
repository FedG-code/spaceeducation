using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HexTileBase
{

    public HexTileBase(HexMap hexMap, int q, int r)
    {
        this.hexMap = hexMap;
        this.Q = q;        // Column
        this.R = r;        // Row
        this.S = -(q + r); // Something

        this.movementCost = 1f; // default

    }

    // Q + R + S = 0
    // S = -(Q + R)
    public readonly int Q;  // Column
    public readonly int R; // Row
    public readonly int S;
    public float movementCost;

    public enum TILE_TYPE { STAR, PLANET, ASTEROID, EVENT, BLACKHOLE, BADPLANET, SPACE };

    // TODO? Does this override unity gameobject?
    public GameObject GameObject { get; set; }


    //public float Moisture;

    private HexMap hexMap;

    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
    float radius = 1f;

    //bool allowWrapEastWest = true;
    //bool allowWrapNorthSouth = false;

    public Vector3 Position()
    {

        float vert = HexHeight() * 0.75f;
        float horiz = HexWidth();

        return new Vector3(
            horiz * (this.Q + this.R / 2f),
            0,
            vert * this.R

            );
    }

    public float HexHeight()
    {
        return radius * 2;
    }

    public float HexWidth()
    {
        return WIDTH_MULTIPLIER * HexHeight();
    }

    public float HexVerticalSpacing()
    {
        return HexHeight() * 0.75f;
    }

    public float HexHorizontalSpacing()
    {
        return HexWidth();
    }

    public Vector3 PositionFromCamera(Vector3 cameraPosition,
        float numRows, float numColumns)
    {
        float mapHeight = numRows * HexVerticalSpacing();

        Vector3 position = Position();

        if (hexMap.allowWrapEastWest)
        {

            float mapWidth = numColumns * HexHorizontalSpacing();

            float howManyWidthsFromCamera = (position.x - cameraPosition.x) / mapWidth;

            if (howManyWidthsFromCamera > 0)
                howManyWidthsFromCamera += 0.5f;
            else
                howManyWidthsFromCamera -= 0.5f;

            int howManyWidthToFix = (int)howManyWidthsFromCamera;

            position.x -= howManyWidthToFix * mapWidth;
        }

        if (hexMap.allowWrapNorthSouth)
        {

            float mapWidth = numColumns * HexHorizontalSpacing();

            float howManyHeightsFromCamera = (position.x - cameraPosition.z) / mapHeight;

            if (howManyHeightsFromCamera > 0)
                howManyHeightsFromCamera += 0.5f;
            else
                howManyHeightsFromCamera -= 0.5f;

            int howManyHeightsToFix = (int)howManyHeightsFromCamera;

            position.z -= howManyHeightsToFix * mapWidth;
        }
        return position;

    }
    /* public static float Distance (Hex a, Hex b)
     {
         int dQ = Mathf.Abs(a.Q-b.Q);
         if(a.hexMap.allowWrapEastWest){
         if(dQ> hexMap.numColumns/2)
         dQ = hexMap.numColumns - dQ;
         }

         int dR = Mathf.Abs(a.R-b.R);
         if(a.hexMap.allowWrapNorthSouth){
         if(dR> hexMap.numRows/2)
         dR = hexMap.numRows - dR;
         }
         return
             Mathf.Max
             (
                 dQ,
                 dR,
                 Mathf.Abs(a.S-b.S)
             );
     }
     */
}
