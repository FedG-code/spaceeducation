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
                    return new HexTileAsteroid(map, q, r, gameObject);
                }
            case HexTileBase.TILE_TYPE.BLACKHOLE:
                {
                    return new HexTileBlackhole(map, q, r, gameObject);
                }
            case HexTileBase.TILE_TYPE.STAR:
                {
                    return new HexTileStar(map, q, r, gameObject);
                }
            case HexTileBase.TILE_TYPE.PLANET:
                {
                    return new HexTilePlanet(map, q, r, gameObject);
                }
            case HexTileBase.TILE_TYPE.EVENT:
                {
                    return new HexTileEvent(map, q, r, gameObject);
                }
            default:
                {
                    return new HexTileSpace(map, q, r);
                }
        }

    }
}
