using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkController : MonoBehaviour
{
    public GameObject fireworkParticleModel;
    public GameObject fireworkContainer;
    public int numParticles;

    private float fireworkDuration = 3f;
    private float gapBetweenFireworks = 2f;
    private float timeTaken = 0f;

    private bool runningFirework = true;
    private List<GameObject> particles;

    // Start is called before the first frame update
    void Start()
    {
        setupParticles();
    }

    // Update is called once per frame
    void Update()
    {
        timeTaken += Time.deltaTime;
        if (timeTaken >= fireworkDuration & runningFirework) {
            cleanupParticles();
            runningFirework = false;
            timeTaken = 0f;
        } else if (runningFirework) {
            moveParticles();
        } else if (!runningFirework & timeTaken >= gapBetweenFireworks) {
            // rerun the firework after a set number of time passes
            setupParticles();
            runningFirework = true;
            timeTaken = 0f;
        }
    }

    void moveParticles() {
        if (timeTaken < fireworkDuration / 2) {
            for (int i = 0; i < particles.Count; i++) {
                // make half the particles experience less force
                float forceMultiplier;
                if (i % 2 == 0) {
                    forceMultiplier = 2f;
                } else {
                    forceMultiplier = 1.5f;
                }

                // calculate a range of directions in a circle around centrepoint
                float directionY = (Mathf.Cos(i * (2 * Mathf.PI) / particles.Count) + 1.1f) * 2;
                float directionX = Mathf.Sin(i * (2 * Mathf.PI) / particles.Count);
                Vector3 direction = new Vector3(directionX, directionY, 0.5f) * forceMultiplier;

                //particles[i].transform.localPosition = direction;

                particles[i].GetComponent<Rigidbody>().AddForce(direction, ForceMode.Force);
            }
        }
    }

    void setupParticles() {
        particles = new List<GameObject>();
    
        for (int i = 0; i < numParticles; i++) {
            // instantiate a list of firework particles
            GameObject particle = Instantiate(fireworkParticleModel);
            particle.transform.parent = fireworkContainer.transform;
            particle.transform.localPosition = new Vector3(0f, 0f, 0f);
            particles.Add(particle);
        }
    }

    void cleanupParticles() {
        foreach (GameObject particle in particles) {
            Destroy(particle);
        }
    }
}
