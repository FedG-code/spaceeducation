using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class HexTilePlanet : HexTileBase, IHexTile
{
    public HexTilePlanet(HexMap hexMap, int q, int r, GameObject gameObject)
        : base(hexMap, q, r)
    {
        MeshRenderer mr = gameObject.GetComponentInChildren<MeshRenderer>();
        mr.material = hexMap.MatPlanet;

        this.movementCost = 10f; // default
    }

    public bool Populated { get; set; }

    //public void DoAction(playerObject)
    public void DoAction()
    {
        if (this.Populated)
        {
            //Pop-up scenario that relates to a populated planet with a qustion to answer which then gives you some sort of resource
        }
        else if (!this.Populated)
        {
            //if playerObject.MiningTools.contains("Big Drill")
            //    {
            //    playerObject.Minerals.add("100 tonnes of iron");

            //}
            //Give some metal resource with the amount dependent on the part of the tech tree they are on. Further=more rescources
            //possibly could include questions on the planets but need to figure out what to ask...
        }

        else
        {
            // i.e bad planet. take some damage/ lose some fuel points/ lose some other resource points...
        }


    }



    public bool PlayerCanMoveHere()
    {
        return true;
    }
}
