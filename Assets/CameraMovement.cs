using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Code for the camera movement was from the tutorial in
https://www.youtube.com/watch?v=f473C43s8nE
*/
public class CameraMovement : MonoBehaviour
{
    public Transform orientation;

    public float xSensitivity;
    public float ySensitivity;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
