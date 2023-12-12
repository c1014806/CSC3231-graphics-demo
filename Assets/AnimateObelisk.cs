using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObelisk : MonoBehaviour
{
    public Transform container;
    public Transform swirlingParticleModel;
    public Transform centrePiece;

    public float animationDuration;
    public float peakIntensity;
    public float particleHeightMultiplier;

    private MeshRenderer obeliskRenderer;
    private float timeElapsed = 0.0f;
    private float itensityChangePerSecond;
    private List<Transform> swirlingParticles = new List<Transform>();
    
    // Start is called before the first frame update
    void Start()
    {
        obeliskRenderer = centrePiece.GetComponent<MeshRenderer>();
        // spend half the time increasing intensity, half decreasing
        itensityChangePerSecond = peakIntensity / (animationDuration / 2);

        // create prefabs with desired starting position relative to parent such that each are a quarter of a circle apart
        List<Vector3> startingPositions = new List<Vector3> {
            new Vector3(4, 0, 0), 
            new Vector3(0, 0, 4), 
            new Vector3(-4, 0, 0), 
            new Vector3(0, 0, -4)
        };
        for (int i = 0; i < 4; i++) {
            Transform particle = Instantiate(swirlingParticleModel, new Vector3(0,0,0), Quaternion.identity) as Transform;
            particle.transform.parent = container.transform;
            particle.localPosition = startingPositions[i];

            swirlingParticles.Add(particle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //reset time after animation completes
        timeElapsed += Time.deltaTime;
        if (timeElapsed >= animationDuration) {
            timeElapsed = 0.0f;
        }
        
        animateObeliskMaterialIntensity();
        animateParticles();
        rotateContainer();
    }

    private void rotateContainer() {
        container.Rotate(0, 200 * Time.deltaTime, 0);
    }

    private void animateObeliskMaterialIntensity() {
        float intensity;
        if (timeElapsed <= animationDuration / 2) {
            //increase fir first half
            intensity = itensityChangePerSecond * timeElapsed;
        } else {
            //decrease for second half
            intensity = itensityChangePerSecond * (animationDuration - timeElapsed);
        }

        Color color = new Vector4(0.516f*intensity, 0.0f, 0.809f * intensity, intensity);
        obeliskRenderer.material.SetColor("_EmissionColor", color);
    }

    private void animateParticles() {
        // use sin value to allow the particles to move up and down smoothly
        float radiansThroughRotation = 2 * Mathf.PI / animationDuration * timeElapsed;

        float offset = 0;
        foreach (Transform particle in swirlingParticles) {
            float y = (float) Mathf.Sin(radiansThroughRotation + offset) * particleHeightMultiplier;
            particle.localPosition = new Vector3(particle.localPosition.x, y, particle.localPosition.z);

            // use offset to make each particle a quarter of a wave apart
            offset += Mathf.PI / 2;
        }
    }
}
