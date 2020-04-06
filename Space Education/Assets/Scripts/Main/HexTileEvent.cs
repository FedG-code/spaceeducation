using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HexTileEvent : HexTileBase, IHexTile
{
    public HexTileEvent(HexMap hexMap, int q, int r, GameObject gameObject)
        : base(hexMap, q, r)
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        mr.material = hexMap.MatEvent;
        hasAction = true;

    }
	public void DoAction() {
      // Have random scenario pop up that gives you a boost of some description
		Debug.Log("There is something to do");
    }


    public bool PlayerCanMoveHere()
    {
        return true;
    }
}
