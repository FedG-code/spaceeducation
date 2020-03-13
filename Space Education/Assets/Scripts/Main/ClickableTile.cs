using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour
{
    public int tileQ;
    public int tileR;

    void OnMouseUp() {
        Debug.Log("Clicko World!");
    }
}
