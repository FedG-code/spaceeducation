using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HexTileBlackhole : HexTileBase, IHexTile
{
    public HexTileBlackhole(HexMap hexMap, int q, int r, GameObject gameObject)
     : base(hexMap, q, r)
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        this.movementCost = 99999999999999999999999999f;
        mr.material = hexMap.MatBlackHole;
    }

	public void enterHex()
    { //Pop-up that you cant move there as it's a black hole...
    }
    public bool PlayerCanMoveHere()
    {
        return false;
    }


}
