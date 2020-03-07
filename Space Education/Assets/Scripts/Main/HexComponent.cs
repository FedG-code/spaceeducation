using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexComponent : MonoBehaviour
{
    public HexTileBase Hex;
    public HexMap HexMap;
    public void UpdatePosition()
    {
        this.transform.position = Hex.PositionFromCamera(
            Camera.main.transform.position,
            HexMap.numRows,
            HexMap.numColumns
            );
    }
}
