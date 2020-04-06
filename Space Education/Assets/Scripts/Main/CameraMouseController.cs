using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMouseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    bool isDraggingCamera = false;
    Vector3 lastMousePosition;

    // Update is called once per frame
    void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //What is the point at which the mouse ray intersects Y = 0
        if (mouseRay.direction.y >= 0)
        {
            Debug.LogError("Why is mouse pointing up?");
            return;
        }
        float rayLength = mouseRay.origin.y / mouseRay.direction.y;
        // Hit Position
        Vector3 hitPos = mouseRay.origin - (mouseRay.direction * rayLength);

        if (Input.GetMouseButtonDown(0))
        {
            // Mouse button just went down -- start a drag.

            isDraggingCamera = true;


            lastMousePosition = hitPos;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            //Mouse button went up, stop drag
            isDraggingCamera = false;
        }

        if (isDraggingCamera)
        {
            Vector3 diff = lastMousePosition - hitPos;
            Camera.main.transform.Translate(diff, Space.World);
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //What is the point at which the mouse ray intersects Y = 0
            if (mouseRay.direction.y >= 0)
            {
                Debug.LogError("Why is mouse pointing up?");
                return;
            }
            rayLength = mouseRay.origin.y / mouseRay.direction.y;
            lastMousePosition = hitPos = mouseRay.origin - (mouseRay.direction * rayLength);
        }

        // Zoom to scrollwheel
        float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
        float minHeight = 2;
        float maxHeight = 20;
        if (Mathf.Abs(scrollAmount) > 0.01f)
        {

            // Move camera towards hitPos
            Vector3 dir = hitPos - Camera.main.transform.position;

            Camera.main.transform.Translate(dir * scrollAmount, Space.World);

            //Make sure there's a min/max y value so we don't zoom past the map
            Vector3 p = Camera.main.transform.position;

            // ignore the zoom action after the zoom is at max position
            if (scrollAmount > 0 || p.y < (maxHeight - 0.1f))
            {
                Camera.main.transform.Translate(dir * scrollAmount, Space.World);
            }
            if (p.y < minHeight)
            {
                p.y = minHeight;
            }

            if (p.y > maxHeight)
            {
                p.y = maxHeight;
            }
            Camera.main.transform.position = p;

            //Change camera angle
            float lowZoom = minHeight + 3;
            float highZoom = maxHeight - 10;



            //if (p.y < lowZoom)
            //{
            //    // camera tilt from 10 to 60 degree
            //    Camera.main.transform.rotation = Quaternion.Euler(
            //        Mathf.Lerp(10, 60, ((p.y - minHeight) / (lowZoom - minHeight) )),
            //        Camera.main.transform.rotation.eulerAngles.y,
            //        Camera.main.transform.rotation.eulerAngles.z
            //        );
            //}
            //else if (p.y > highZoom)
            //{
            //    //camera tilt from 60 to 90 degree
            //    Camera.main.transform.rotation = Quaternion.Euler(
            //        Mathf.Lerp(60, 90, ( (p.y - highZoom) / (maxHeight - highZoom))),
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
        Camera.main.transform.rotation = Quaternion.Euler(
                    Mathf.Lerp(35, 90, Camera.main.transform.position.y / (maxHeight / 1.5f)),
                    Camera.main.transform.rotation.eulerAngles.y,
                    Camera.main.transform.rotation.eulerAngles.z
                    );
    }
}
