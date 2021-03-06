﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HexTileStar : HexTileBase, IHexTile
{
    public HexTileStar(HexMap hexMap, int q, int r, GameObject gameObject)
        : base(hexMap, q, r)
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        mr.material = hexMap.MatStar;
        this.movementCost = 1f; // default
    }


    public void DoAction()
    {

        // Nothing to do here. Can't land on a star...
        // Maybe have a pop-up to say you can't land on the star?...
    }

    public bool PlayerCanMoveHere()
    {
        return false;
    }

}
