using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public HexMap map;
    public int Q;
    public int R;

    void OnMouseUp() {
        Debug.LogFormat("Generatoring a path to {0},{1}",Q,R);
        map.GeneratePathTo(Q, R);
    }
}
