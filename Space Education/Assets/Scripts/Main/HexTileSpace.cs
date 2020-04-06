using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HexTileSpace: HexTileBase, IHexTile
    {
    public HexTileSpace(HexMap hexMap, int q, int r)
        : base(hexMap, q, r)
    {

    }

	public void enterHex() {
        // Nothing to do...
    }

    public bool PlayerCanMoveHere()
    {
        return true;
    }
}
