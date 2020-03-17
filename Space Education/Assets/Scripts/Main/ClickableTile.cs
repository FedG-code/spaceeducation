using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public HexMap map;
    public int Q;
    public int R;

    void OnMouseUp() {
        map.MoveSelectedUnitTo(Q, R);
    }
}
