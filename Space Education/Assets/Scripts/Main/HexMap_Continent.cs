using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap
{
    override public void GenerateMap()
    {
        base.GenerateMap();

        GenerateSun(56, 10, 4);
        //Fed: here comes the skipping we're not concerned with meshes 
        UpdateHexVisuals();
    }
    //Fed: what quill calls elevate area
    void GenerateSun(int q, int r, int radius)
    {

         Hex centerHex = GetHexAt(q, r);
        //centerHex.Elevation = 1f;
        centerHex.Elevation = 0.5f;

        /*Hex[] areaHexes = GetHexesWithinRadiusOf(centerHex, radius);

        foreach (Hex h in areaHexes)
        {
            h.Elevation = 1f;
        }
        */
    }
    
}
