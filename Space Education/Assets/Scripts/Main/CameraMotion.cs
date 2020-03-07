using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        oldPosition = this.transform.position;
    }
    Vector3 oldPosition;
    // Update is called once per frame
    /**
        void Update()
        {
            //TODO: click and drag, wasd, zoom
            CheckIfCameraMoved();
        }
    **/

    public void PanToHex(HexTileBase hex)
    {

    }
    HexComponent[] hexes;
     void CheckIfCameraMoved()
    {
        if (oldPosition != this.transform.position)
        {
            //something moved the camera
            oldPosition = this.transform.position;
            if(hexes == null)
            hexes = GameObject.FindObjectsOfType<HexComponent>();

            foreach(HexComponent hex in hexes)
            {
                hex.UpdatePosition();
            }
        }
    }

}
