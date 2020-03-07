using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Factory class to create the different tile types
public class TileFactory
{
    public HexTileBase GetTile(HexMap map, int q, int r, HexTileBase.TILE_TYPE tileType, GameObject gameObject)
    {
        switch (tileType)
        {
            case HexTileBase.TILE_TYPE.ASTEROID:
                {
                    return new AsteroidTile(map, q, r, gameObject);
                }
            case HexTileBase.TILE_TYPE.BLACKHOLE:
                {
                    return new BlackholeTile(map, q, r, gameObject);
                }
            case HexTileBase.TILE_TYPE.STAR:
                {
                    return new StarTile(map, q, r, gameObject);
                }
            case HexTileBase.TILE_TYPE.PLANET:
                {
                    return new PlanetTile(map, q, r, gameObject);
                }
            case HexTileBase.TILE_TYPE.EVENT:
                {
                    return new EventTile(map, q, r, gameObject);
                }
            default:
                {
                    return new SpaceTile(map, q, r);
                }
        }

    }
}

