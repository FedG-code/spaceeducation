using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap
{
    override public void GenerateMap()
    {
        base.GenerateMap();

        GenerateTile(0, 0, 0,HexTileBase.TILE_TYPE.SPACE);
        //small sun
        GenerateTile(8, 23, 2,HexTileBase.TILE_TYPE.STAR);

        //planet tile
        GenerateTile(8, 26, 0,HexTileBase.TILE_TYPE.PLANET);

        GenerateTile(6, 30, 0,HexTileBase.TILE_TYPE.PLANET);

        GenerateTile(12, 29, 0,HexTileBase.TILE_TYPE.PLANET);

        GenerateTile(15, 24,0,HexTileBase.TILE_TYPE.EVENT);

        GenerateTile(17, 32, 3,HexTileBase.TILE_TYPE.BLACKHOLE);

        GenerateTile(5 ,32, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(5,33, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(6,33, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(6, 34, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(7,34, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(8, 34, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(9, 33, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(10, 33, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(11, 33, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(12, 33, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(12, 34, 0,HexTileBase.TILE_TYPE.ASTEROID);

        GenerateTile(13, 34, 0,HexTileBase.TILE_TYPE.ASTEROID);

        // MoveSelectedUnitTo(9,26);
        // SpawnUnitAt(selectedUnit, 9, 26);

        //Debug.LogError(override ok);

        //Fed: here comes the skipping we're not concerned with meshes
       // UpdateHexVisuals();
    }
    //Fed: what quill calls elevate area I called Generate Tile
    //Fed: tiles seem to be spawning at q-1 from the coordinates we give, odd
    void GenerateTile(int q, int r, int range, HexTileBase.TILE_TYPE tiletype)
    {
        // New Tile Fasctory to create the tiles
        TileFactory factory = new TileFactory();

        // Get the Space tile at the location specified
        HexTileBase centerHex = GetHexAt(q+1, r);
        // Get a new tile of the specified type from the factory
        HexTileBase newTile = factory.GetTile(this, centerHex.Q, centerHex.R, tiletype, centerHex.GameObject);
        // set the new tile's gameObject to that of the tile being replaced
        newTile.GameObject = centerHex.GameObject;
        // Replace the tile
        hexes[centerHex.Q, centerHex.R] = newTile;

        // Get the tiles around it within the specified radius
        List<HexTileBase> areaHexes = GetHexesWithinRadiusOf(centerHex, range);

        // Replace those tiles with tiles of the type specified
        foreach (HexTileBase h in areaHexes)
        {
            HexTileBase surroundingTile = factory.GetTile(this, h.Q, h.R, tiletype, h.GameObject);
            surroundingTile.GameObject = h.GameObject;
            hexes[h.Q, h.R] = surroundingTile;
        }
    }

}
