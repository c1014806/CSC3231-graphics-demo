using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkController : MonoBehaviour
{
    public GameObject fireworkParticleModel;
    public GameObject fireworkContainer;

    private float fireworkDuration = 3f;
    private float timeTaken = 0f;

    private List<GameObject> particles;
    private List<Color> possibleColors = new List<Color> {
        new Color(0f / 255f, 255f / 255f, 0f / 255f), // green
        new Color(255f / 255f, 0f / 255f, 0f / 255f), // red
        new Color(0f / 255f, 0f / 255f, 255f / 255f), // blue
        new Color(255f / 255f, 0f / 255f, 255f / 255f) // pink
    };

// Start is called before the first frame update
void Start()
    {
        setupParticles();
    }

    // Update is called once per frame
    void Update()
    {
        timeTaken += Time.deltaTime;

        // destroy all game objects when the firework has finished to save resources
        if (timeTaken >= fireworkDuration) {
            cleanupParticles();
            Destroy(gameObject);
        } else {
            moveParticles();
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
                float directionY = (Mathf.Cos(i * (2 * Mathf.PI) / particles.Count) + 1.1f) * 4;
                float directionX = Mathf.Sin(i * (2 * Mathf.PI) / particles.Count);
                Vector3 direction = new Vector3(directionX, directionY, 0.5f) * forceMultiplier;

                particles[i].GetComponent<Rigidbody>().AddForce(direction, ForceMode.Force);
            }
        }
    }

    void setupParticles() {
        // pick random colour for the firework
        int index = Random.Range(0, 4);
        int numParticles = Random.Range(30, 80);

        particles = new List<GameObject>();
        for (int i = 0; i < numParticles; i++) {
            // instantiate a list of firework particles
            GameObject particle = Instantiate(fireworkParticleModel);
            particle.transform.parent = fireworkContainer.transform;
            particle.transform.localPosition = new Vector3(0f, 0f, 0f);
            particle.GetComponent<TrailRenderer>().startColor = possibleColors[index];
            particle.GetComponent<TrailRenderer>().endColor = possibleColors[index];
            particles.Add(particle);
        }
    }

    void cleanupParticles() {
        foreach (GameObject particle in particles) {
            Destroy(particle);
        }
    }
}
