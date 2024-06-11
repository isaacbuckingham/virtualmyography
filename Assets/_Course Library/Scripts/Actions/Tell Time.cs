using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellTime : MonoBehaviour
{
    public Transform hourHand;
    public Transform minuteHand;
    public Transform secondHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current time
        DateTime currentTime = DateTime.Now;

        // Calculate the rotation for the hour hand (360 degrees / 12 hours)
        float hourDegrees = (currentTime.Hour % 12) * 30 + currentTime.Minute * 0.5f;
        hourHand.localRotation = Quaternion.Euler(hourDegrees, 0, 0);

        // Calculate the rotation for the minute hand (360 degrees / 60 minutes)
        float minuteDegrees = currentTime.Minute * 6 + currentTime.Second * 0.1f;
        minuteHand.localRotation = Quaternion.Euler(minuteDegrees, 0, 0);

        // Calculate the rotation for the second hand (360 degrees / 60 seconds)
        float secondDegrees = currentTime.Second * 6;
        secondHand.localRotation = Quaternion.Euler(secondDegrees, 0, 0);
    }
}