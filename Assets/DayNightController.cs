using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public Material skyboxPrefab;
    public GameObject directionalLight;
    public float dayDurationSeconds;

    private Material skyboxInstance;
    private float transitionDuration;
    private float timeSinceTransitionBegan;
    private float timeSinceDayBegan;

    // Start is called before the first frame update
    void Start()
    {
        skyboxInstance = Instantiate(skyboxPrefab);
        RenderSettings.skybox = skyboxInstance;

        transitionDuration = dayDurationSeconds / 8f;
        timeSinceTransitionBegan = 0f;
        timeSinceDayBegan = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceDayBegan += Time.deltaTime;
        
        // reset day if its over
        if (timeSinceDayBegan >= dayDurationSeconds) {
            timeSinceDayBegan = 0f;
            timeSinceTransitionBegan = 0f;
        }

        float eighthOfDay = dayDurationSeconds / 8;
        if (timeSinceDayBegan >= 3 * eighthOfDay & timeSinceDayBegan < 4 * eighthOfDay) {
            transitionDayToNight();
        } else if (timeSinceTransitionBegan != 0f & timeSinceDayBegan >= 4 * eighthOfDay & timeSinceDayBegan < 7 * eighthOfDay) {
            // reset the transition timer if we finished transitioning to night
            timeSinceTransitionBegan = 0f;            
        } else if (timeSinceDayBegan >= 7 * eighthOfDay) {
            transitionNightToDay();
        }
    }

    private void transitionDayToNight() {
        timeSinceTransitionBegan += Time.deltaTime;
        float eighthOfDay = dayDurationSeconds / 8;
        float proportionOfTransition = timeSinceTransitionBegan / eighthOfDay;

        transitionThickness(proportionOfTransition);

        // smoothly transition light intensity down to 0.3f
        Light light = directionalLight.GetComponent<Light>();
        light.intensity = 1f - (0.7f * proportionOfTransition);

        // transition between tint colour of the sky
        float R = 170f - (170f * proportionOfTransition);
        float G = R;
        float B = 170f - ((170f - 27f) * proportionOfTransition);
        skyboxInstance.SetColor("_SkyTint", new Color(R / 255f, G / 255f, B / 255f));

        // transition exposure 
        float exposure = 2.1f - ((2.1f - 0.51f) * proportionOfTransition);
        skyboxInstance.SetFloat("_Exposure", exposure);
    }

    private void transitionNightToDay() {
        timeSinceTransitionBegan += Time.deltaTime;
        float eighthOfDay = dayDurationSeconds / 8;
        float proportionOfTransition = timeSinceTransitionBegan / eighthOfDay;

        transitionThickness(proportionOfTransition);

        // smoothly transition light intensity up to 1f
        Light light = directionalLight.GetComponent<Light>();
        light.intensity = 0.3f + (0.7f * proportionOfTransition);

        // transition between tint colour of the sky
        float R = (170f * proportionOfTransition);
        float G = R;
        float B = 27f + ((170f - 27f) * proportionOfTransition);
        skyboxInstance.SetColor("_SkyTint", new Color(R / 255f, G / 255f, B / 255f));

        // transition exposure 
        float exposure = 0.51f + ((2.1f - 0.51f) * proportionOfTransition);
        skyboxInstance.SetFloat("_Exposure", exposure);
    }

    private void transitionThickness(float proportionOfTransition) {
        // bring thickness up for first half of transition and back down again to simulate
        // dawn or evening colours
        float thickness;
        if (proportionOfTransition <= 0.5) {
            thickness = 0.62f + ((2.0f - 0.62f) * 2 * proportionOfTransition);
        } else if (proportionOfTransition <= 1.0f) {
            thickness = 2.0f - ((2.0f - 0.62f) * 2 * (proportionOfTransition - 0.5f));
        } else {
            // don't modify anything if called too late
            return;
        }
        
        skyboxInstance.SetFloat("_AtmosphereThickness", thickness);
        Debug.Log(skyboxInstance.GetFloat("_AtmosphereThickness"));
    }
}
