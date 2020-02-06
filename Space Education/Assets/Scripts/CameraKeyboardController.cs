using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKeyboardController : MonoBehaviour {

    // Use this for initialization
    void Start () {
        
    }

    float moveSpeed = 3.5f;
    
    // Update is called once per frame
    void Update () {
        
        Vector3 translate = new Vector3
            (
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            );

        this.transform.Translate( translate * moveSpeed * Time.deltaTime * (1 + this.transform.position.y / 2), Space.World);

    }
}
