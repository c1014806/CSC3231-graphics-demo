using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModifyUIOverlay : MonoBehaviour
{
    public TextMeshProUGUI FrameRate;
    public TextMeshProUGUI MemoryUse;

    private float previousFramerate = 0.0f;
    private float timeSinceLastCalculation = 0.0f;
    private int framesSinceLastCalculation = 0;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        FrameRate.text = "Framerate: - fps";
        MemoryUse.text = "Memory usage: - MB";
    }

    // Update is called once per frame
    void Update()
    {
        ShowFrameRate();
        ShowMemoryUsage();
    }

    void ShowMemoryUsage() {
        //get memory use in MB by dividing bytes used by 1*10^6
        long memoryUsedMB = System.GC.GetTotalMemory(false) / (long) 1e6;
        MemoryUse.text = "Memory usage: " + memoryUsedMB + "MB";
    }

    void ShowFrameRate() {
        CalculateFrameRate();

        string message = "Framerate: " + previousFramerate + " fps";
        FrameRate.text = message;
    }

    void CalculateFrameRate() {
        // calculate avg framerate every 0.3 seconds
        timeSinceLastCalculation += Time.deltaTime;
        framesSinceLastCalculation += 1;

        if (timeSinceLastCalculation >= 0.3f) {
            previousFramerate = (float) framesSinceLastCalculation / timeSinceLastCalculation;
            timeSinceLastCalculation = 0f;
            framesSinceLastCalculation = 0;
        }
    }
}
