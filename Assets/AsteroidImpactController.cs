using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidImpactController : MonoBehaviour
{
    public GameObject preImpactModel;
    public GameObject postImpactModel;
    public GameObject asteroidModel;
    public float timeToImpact;

    private GameObject currentGameObject;
    private GameObject asteroid;
    private float timeElapsed;
    private bool simulationComplete = false;
    private Vector3 startingPos = new Vector3(-60f, 1250f, -1085f);
    private Vector3 finishPos = new Vector3(-58.49545f, 60.31f, -55.94f);

    // Start is called before the first frame update
    void Start()
    {       
        currentGameObject = Instantiate(preImpactModel, new Vector3(362f, 11f, 334.5f), Quaternion.identity);
        asteroid = Instantiate(asteroidModel, new Vector3(362f, 11f, 334.5f), Quaternion.identity);
        asteroid.transform.parent = currentGameObject.transform;
        asteroid.transform.localPosition = startingPos;
        asteroid.transform.localScale = new Vector3(3, 3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeToImpact && !simulationComplete) {
            Destroy(asteroid);
            Destroy(currentGameObject);
            currentGameObject = Instantiate(postImpactModel, new Vector3(362f, 11f, 334.5f), Quaternion.identity) as GameObject;
            simulationComplete = true;
        } else if (!simulationComplete) {
            float proportionOfRoute = timeElapsed / timeToImpact;
            float newX = startingPos.x - ((startingPos.x - finishPos.x) * proportionOfRoute);
            float newY = startingPos.y - ((startingPos.y - finishPos.y) * proportionOfRoute);
            float newZ = startingPos.z - ((startingPos.z - finishPos.z) * proportionOfRoute);

            asteroid.transform.localPosition = new Vector3(newX, newY, newZ);
        }
    }
}
