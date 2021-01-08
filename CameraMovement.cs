using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    private float rotY = 0.0f;
    private float rotX = 0.0f;




    // Start is called before the first frame update
    void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;

        rotY = rot.y;
        rotX = rot.x;
    }

    // Update is called once per frame
    void Update()
    {


        
    }

    void LookAround()
    {
        float mousex = Input.GetAxis("Mouse X");
        float mousey = Input.GetAxis("Mouse Y");

        rotY += mousex * mouseSensitivity * Time.deltaTime;
        rotX += mousey * mouseSensitivity * Time.deltaTime;

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

}

