using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObelisk : MonoBehaviour
{
    public Transform container;
    public Transform centrePiece;

    public float animationDuration;
    public float peakIntensity;

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
        container.Rotate(0, 100 * Time.deltaTime, 0);
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
}
