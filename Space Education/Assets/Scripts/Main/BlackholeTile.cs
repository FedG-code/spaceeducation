using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BlackholeTile : HexTileBase, IHexTile
{
    public BlackholeTile(HexMap hexMap, int q, int r, GameObject gameObject)
     : base(hexMap, q, r)
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        mr.material = hexMap.MatBlackHole;
    }

    public void DoAction()
    { //Pop-up that you cant move there as it's a black hole...
    }
    public bool PlayerCanMoveHere()
    {
        return false;
    }


}


