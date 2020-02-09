using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    bool isDraggingCamera = false;
    Vector3 LastMousePosition;

    // Update is called once per frame
    void Update()
    {
        // Mouse to Drag the map  
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (mouseRay.direction.y >= 0)
        {
            return;
        }
        float rayLength = (mouseRay.origin.y / mouseRay.direction.y);
        Vector3 hitPos = mouseRay.origin - (mouseRay.direction * rayLength);

        if (Input.GetMouseButtonDown(0))
        {
            isDraggingCamera = true;

            
            LastMousePosition = hitPos;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraggingCamera = false; 
        }

        if (isDraggingCamera)
        {
            Vector3 diff = LastMousePosition - hitPos;
            Camera.main.transform.Translate(diff, Space.World);
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (mouseRay.direction.y >= 0)
            {
                return;
            }
            rayLength = (mouseRay.origin.y / mouseRay.direction.y);
            LastMousePosition = mouseRay.origin - (mouseRay.direction * rayLength); 
        }

        // Scrollwheel to zoom
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        float minHeight = 2;
        float maxHeight = 20;
        if( Mathf.Abs(scrollAmount) > 0.01f)
        {
             
            Vector3 dir = hitPos - Camera.main.transform.position;

            Vector3 p = Camera.main.transform.position;

            if (scrollAmount > 0 || p.y < (maxHeight-0.1f))
            {
                Camera.main.transform.Translate(dir * scrollAmount, Space.World);
            }
            p = Camera.main.transform.position;
            if (p.y < minHeight)
            {
                p.y = minHeight;
            }
            if (p.y > maxHeight)
            {
                p.y = maxHeight;
            }
            Camera.main.transform.position = p;

            

            //float lowZoom = minHeight+3;
            //float highZoom = maxHeight-3;
            //if (p.y < lowZoom)
            //{
            //    Camera.main.transform.rotation = Quaternion.Euler(
            //        Mathf.Lerp(10,60,(p.y - minHeight) / lowZoom-minHeight),
            //        Camera.main.transform.rotation.eulerAngles.y,
            //        Camera.main.transform.rotation.eulerAngles.z

            //        );
            //}
            //else if(p.y > highZoom)
            //{ 
            //    Camera.main.transform.rotation = Quaternion.Euler(
            //        Mathf.Lerp(60, 90, ((p.y-maxHeight)) / (maxHeight-highZoom)),
            //        Camera.main.transform.rotation.eulerAngles.y,
            //        Camera.main.transform.rotation.eulerAngles.z

            //        );
            //}
            //else
            //{
            //    Camera.main.transform.rotation = Quaternion.Euler(
            //        60,
            //        Camera.main.transform.rotation.eulerAngles.y,
            //        Camera.main.transform.rotation.eulerAngles.z

            //        );
            //}



        }

        // Change camera angle
        //Lerp(35 is the minimum, 90 is the maximum)

        

        Camera.main.transform.rotation = Quaternion.Euler(
                Mathf.Lerp(35, 90, (Camera.main.transform.position.y) / (maxHeight / 1.5f)),
                Camera.main.transform.rotation.eulerAngles.y,
                Camera.main.transform.rotation.eulerAngles.z

                );

    }
}
