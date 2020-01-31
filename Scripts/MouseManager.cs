using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { }
        
    

    // Update is called once per frame
    void Update() {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}


        //if (Input.GetMouseButtonDown(0))
        //{
        //    // Check if the mouse was clicked over a UI element
        //    if (!EventSystem.current.IsPointerOverGameObject())
        //    {
        //        Debug.Log("Did not Click on the UI");
        //    }
        //}


        // check if the mouse is over a Unity UI Element
        //Debug.Log("Mouse Position: " + Input.mousePosition);
        //Vector3 worldPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //Debug.Log("World Point: " + worldPoint);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo)) {
            GameObject ourHitObject = hitInfo.collider.transform.gameObject;
            //Debug.Log("Raycast hit: " + hitInfo.collider.transform.parent.name);

            if (Input.GetMouseButton(0)) {

               MeshRenderer mr = ourHitObject.GetComponentInChildren<MeshRenderer>();

                if(mr.material.color == Color.red)
                {
                    mr.material.color = Color.black ;
                }
                else
                {
                    mr.material.color = Color.red;
                }
            }
        }
    }
}
