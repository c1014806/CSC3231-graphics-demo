using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireworksDisplayController : MonoBehaviour
{
    public GameObject fireworkPrefab;
    public Transform fireworkShowContainer;
    public float timeBetweenLaunches;

    private float timeSinceLastLaunch = 0f;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastLaunch += Time.deltaTime;
        if (timeSinceLastLaunch > timeBetweenLaunches )
        {
            timeSinceLastLaunch = 0f;
            LaunchFirework();
        }
    }

    private void LaunchFirework()
    {
        // instantiate the instance of firework, this will self destruct on completion so no need
        // to keep track after setup
        GameObject firework = Instantiate(fireworkPrefab) as GameObject;
        firework.transform.parent = fireworkShowContainer;

        // give it a random pos in relation to the showContainer
        int x = Random.Range(-30, 30);
        int y = Random.Range(-15, 15);
        int z = Random.Range(-10, 10);
        firework.transform.localPosition = new Vector3(x, y, z);
    }
}
