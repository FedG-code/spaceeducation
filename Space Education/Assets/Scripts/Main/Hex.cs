using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hex {

   

    public Hex(int q, int r)
    {
        this.Q = q;
        this.R = r;
        this.S = -(q + r);

    }

    // Q + R + S = 0
    // S = -(Q + R)
    public readonly int Q;  // Column
    public readonly int R; // Row
    public readonly int S;

    public float Elevation;
    //public float Moisture;

    static readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;
    float radius = 1f;

    bool allowWrapEastWest = true;
    bool allowWrapNorthSouth = false;

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

        if (allowWrapEastWest)
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

        if (allowWrapNorthSouth)
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

}
