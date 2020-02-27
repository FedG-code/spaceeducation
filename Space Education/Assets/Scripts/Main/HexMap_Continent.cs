using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap
{
    override public void GenerateMap()
    {
        base.GenerateMap();

        //small sun
        GenerateTile(8, 23, 2,HEX.TILE_TYPE.STAR);
        //planet tile
        GenerateTile(8, 26, 1,Hex.TILE_TYPE.PLANET);

        GenerateTile(6, 30, 1,Hex.TILE_TYPE.PLANET);

        GenerateTile(12, 29, 1,Hex.TILE_TYPE.PLANET);

        GenerateTile(15, 24, 1,Hex.TILE_TYPE.EVENT);

        GenerateTile(17, 32, 3,Hex.TILE_TYPE.BLACKHOLE);

        GenerateTile(5 ,32, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(5,33, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(6,33, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(6, 34, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(7,34, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(8, 34, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(9, 33, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(10, 33, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(11, 33, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(12, 33, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(12, 34, 1,Hex.TILE_TYPE.ASTEROID);

        GenerateTile(13, 34, 1,Hex.TILE_TYPE.ASTEROID);
        //Debug.LogError(override ok);

        //Fed: here comes the skipping we're not concerned with meshes
        UpdateHexVisuals();
    }
    //Fed: what quill calls elevate area I called Generate Tile
    //Fed: tiles seem to be spawning at q-1 from the coordinates we give, odd
    void GenerateTile(int q, int r, int range, Hex.TILE_TYPE tiletype)
    {

        Hex centerHex = GetHexAt(q, r);
        //centerHex.Elevation = 1f;

        //Debug.LogError("Generate sun okay");
        Hex[] areaHexes = GetHexesWithinRadiusOf(centerHex, range);

        foreach (Hex h in areaHexes)
        {
            h.tiletype = tiletype;
        }

    }

}
