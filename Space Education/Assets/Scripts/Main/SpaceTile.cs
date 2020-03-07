using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SpaceTile: HexTileBase, IHexTile
    {
    public SpaceTile(HexMap hexMap, int q, int r)
        : base(hexMap, q, r)
    {

    }

    public void DoAction()
    {
        // Nothing to do...
    }

    public bool PlayerCanMoveHere()
    {
        return true;
    }
}
