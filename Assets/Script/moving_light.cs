using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; // Directional light for the sun
    public float dayLengthInMinutes = 2f; // Length of a full day in real-world minutes (e.g., 2 minutes)

    // Define time
    private float timeSpeed; // Speed at which the time progresses
    private float currentTimeOfDay = 0f; // 0 = start of the day, 1 = end of the day (24 hours cycle)
    private float intensityMultiplier; // Factor for light intensity

    void Start()
    {
        // Calculate the speed of time progression based on the length of the day in real-world minutes
        timeSpeed = 1f / (dayLengthInMinutes * 60f); // Converts real-time to game-time
    }

    void Update()
    {
        UpdateTimeOfDay();
        RotateSun();
        AdjustLightIntensity();
    }

    // Update the time of the day based on the speed of time
    private void UpdateTimeOfDay()
    {
        currentTimeOfDay += timeSpeed * Time.deltaTime;
        if (currentTimeOfDay >= 1f) // Reset the cycle when the day ends (full cycle complete)
        {
            currentTimeOfDay = 0f;
        }
    }

 
    private void RotateSun()
    {
        float sunAngle = currentTimeOfDay * 360f; // Sun will rotate full 360 degrees in a day
        directionalLight.transform.localRotation = Quaternion.Euler(new Vector3(sunAngle - 90f, 170f, 0f));
    }

    // Adjust the intensity of the sun based on the time of the day (brighter during the day, dimmer during the night)
    private void AdjustLightIntensity()
    {
        if (currentTimeOfDay <= 0.25f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = Mathf.Lerp(0f, 1f, (currentTimeOfDay - 0.75f) * 4f); // From 0.75 to 1.0, light fades out
        }
        else
        {
            intensityMultiplier = Mathf.Lerp(1f, 0f, (currentTimeOfDay - 0.25f) * 4f); // From 0.25 to 0.75, light intensifies
        }

        directionalLight.intensity = intensityMultiplier;
    }
}
