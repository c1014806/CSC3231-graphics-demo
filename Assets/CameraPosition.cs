using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Code for the camera position was from the tutorial in
https://www.youtube.com/watch?v=f473C43s8nE
*/
public class CameraPosition : MonoBehaviour
{
    public Transform playerCameraPosition;

    void Update()
    {
        transform.position = playerCameraPosition.position;
    }
}
