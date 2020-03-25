using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HexTileAsteroid: HexTileBase, IHexTile
    {
    public HexTileAsteroid(HexMap hexMap, int q, int r, GameObject gameObject)
        : base(hexMap, q, r)
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        mr.material = hexMap.MatAsteroid;
        this.movementCost = 10f; // default
    }

    public void DoAction()
    {
        throw new NotImplementedException();
    }

    public bool PlayerCanMoveHere()
    {
        throw new NotImplementedException();
    }
}
