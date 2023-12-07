using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObelisk : MonoBehaviour
{
    public Transform container;
    public Transform centrePiece;
    public Transform particle1;
    public Transform particle2;
    public Transform particle3;
    public Transform particle4;

    public float animationDuration;
    public float peakIntensity;
    public float particleHeightMultiplier;

    private MeshRenderer obeliskRenderer;
    private float timeElapsed = 0.0f;
    private float itensityChangePerSecond;
    
    // Start is called before the first frame update
    void Start()
    {
        obeliskRenderer = centrePiece.GetComponent<MeshRenderer>();
        // spend half the time increasing intensity, half decreasing
        itensityChangePerSecond = peakIntensity / (animationDuration / 2);
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
        container.Rotate(0, 200 * Time.deltaTime, 0);
    }

    private void rotateContainer() {

    }

    private void animateObeliskMaterialIntensity() {
        float intensity;
        if (timeElapsed <= animationDuration / 2) {
            //increase
            intensity = itensityChangePerSecond * timeElapsed;
        } else {
            //decrease 
            intensity = itensityChangePerSecond * (animationDuration - timeElapsed);
        }

        Color color = new Vector4(0.516f*intensity, 0.0f, 0.809f * intensity, intensity);
        obeliskRenderer.material.SetColor("_EmissionColor", color);
    }

    private void animateParticles() {
        float radiansThroughRotation = 2 * Mathf.PI / animationDuration * timeElapsed;

        float particle1y = (float) Mathf.Sin(radiansThroughRotation) * particleHeightMultiplier;
        particle1.localPosition = new Vector3(particle1.localPosition.x, particle1y, particle1.localPosition.z);

        float particle2y = (float) Mathf.Sin(radiansThroughRotation + Mathf.PI / 2) * particleHeightMultiplier;
        particle2.localPosition = new Vector3(particle2.localPosition.x, particle2y, particle2.localPosition.z);

        float particle3y = (float) Mathf.Sin(radiansThroughRotation + Mathf.PI) * particleHeightMultiplier;
        particle3.localPosition = new Vector3(particle3.localPosition.x, particle3y, particle3.localPosition.z);

        float particle4y = (float) Mathf.Sin(radiansThroughRotation + Mathf.PI * 3 / 2) * particleHeightMultiplier;
        particle4.localPosition = new Vector3(particle4.localPosition.x, particle4y, particle4.localPosition.z);
    }
}
