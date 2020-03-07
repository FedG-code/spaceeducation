﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EventTile : HexTileBase, IHexTile
{
    public EventTile(HexMap hexMap, int q, int r, GameObject gameObject)
        : base(hexMap, q, r)
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        mr.material = hexMap.MatEvent;

    }
    public void DoAction()
    {
      // Have random scenario pop up that gives you a boost of some description

    }


    public bool PlayerCanMoveHere()
    {
        return true;
    }
}