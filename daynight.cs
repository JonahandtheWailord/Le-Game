using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using System;

public class daynight : MonoBehaviour
{
    public float duration = 5f;
    public static float time = 0;
    public static Boolean timepause = false;
    public static string timeperiod = "";
    public static int clockhour;
    
    int offset = 6;
    string suffix = "";
    public static string clocktime;

    [SerializeField] private Gradient gradient;
    private Light2D _light;
    private float _startTime;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timepause)
        {
            time += Time.deltaTime;
        }

        // Calculate the time elapsed since the start time
        var timeElapsed = daynight.time - _startTime;
        // Calculate the percentage based on the sine of the time elapsed
        var percentage = Mathf.Sin(timeElapsed / duration * Mathf.PI * 2) * 0.5f + 0.5f;
        // Clamp the percentage to be between 0 and 1
        percentage = Mathf.Clamp01(percentage);

        _light.color = gradient.Evaluate(percentage);
        
        // Track the duration of a single hour
        float hourDuration = duration / 24;
        // Update clockhour in a cycle that starts at 6 AM
        clockhour = ((int)(time / hourDuration) + offset) % 24;
        //Determine whether the time is AM or PM
        if (clockhour >= 12){
            suffix = " PM";
        }
        else {
            suffix = " AM";
        }

        //Determine time of day based on clockhour
        switch (clockhour)
        {
            case int i when i >= 0 && i < 4:
                timeperiod = "night";
                break;

            case int i when i >= 4 && i < 8:
                timeperiod = "morning";
                break;

            case int i when i >= 8 && i < 16:
                timeperiod = "day";
                break;

            case int i when i >= 16 && i < 20:
                timeperiod = "evening";
                break;

            case int i when i >= 20 && i < 24:
                timeperiod = "night";
                break;
        }

        //Update the time displayed on screen
        switch (clockhour)
        {
            case 0:
            case 12:
                clocktime = "12" + suffix;
                break;

            default:
                clocktime = ((clockhour % 12)) + suffix;
                break;
        }
        
     }
}
