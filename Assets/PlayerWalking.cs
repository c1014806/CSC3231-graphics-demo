using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalking : MonoBehaviour
{
    public Transform playerOrientation;
    private Rigidbody playerRigidBody;

    public float speedMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //ensure player speed is indepent of framerate
        float frameMultiplier = speedMultiplier * Time.deltaTime;
        
        if (Input.GetKey("up") || Input.GetKey(KeyCode.W)) {
            playerRigidBody.AddForce(playerOrientation.forward * frameMultiplier, ForceMode.Force);
        } 
        if (Input.GetKey("down") || Input.GetKey(KeyCode.S)) {
            playerRigidBody.AddForce(-playerOrientation.forward * frameMultiplier, ForceMode.Force);
        } 
        if (Input.GetKey("right") || Input.GetKey(KeyCode.D)) {
            playerRigidBody.AddForce(playerOrientation.right * frameMultiplier, ForceMode.Force);
        } 
        if (Input.GetKey("left") || Input.GetKey(KeyCode.A)) {
            playerRigidBody.AddForce(-playerOrientation.right * frameMultiplier, ForceMode.Force);
        } 
        if (Input.GetKey("space")) {
            playerRigidBody.AddForce(Vector3.up * frameMultiplier, ForceMode.Force);
        }
        if (Input.GetKey("left shift")) {
            playerRigidBody.AddForce(Vector3.down * frameMultiplier, ForceMode.Force);
        }

        // toggle gravity on or off for the player when G is pressed
        // only use the down press so that gravity isnt toggled again until the key is released and re-pressed
        if (Input.GetKeyDown(KeyCode.G)) {
            bool currentGravityStatus = playerRigidBody.useGravity;
            playerRigidBody.useGravity = !currentGravityStatus;
        }
    }
}
 