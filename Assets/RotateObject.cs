using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Transform objectToRotate;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        objectToRotate.Rotate(0, 100 * Time.deltaTime, 0);
    }
}
