using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableTile : MonoBehaviour
{
    public HexMap map;
    public int Q;
    public int R;

    void OnMouseDown() {
        if (!UICollisionDetect())
            map.GeneratePathTo(Q, R);
    }

    private bool UICollisionDetect()
    {
        // Adapted from https://www.youtube.com/watch?v=QL6LOX5or84
        PointerEventData cursorPos = new PointerEventData(EventSystem.current);
        cursorPos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(cursorPos, results);
        return results.Count > 0;
    }

}
