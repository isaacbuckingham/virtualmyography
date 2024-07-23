using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyoEvents : MonoBehaviour
{
    public EMGRawReader emgRawReader;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        emgRawReader = FindObjectOfType<EMGRawReader>();
        rend = GetComponent<Renderer>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (emgRawReader.readVal == "2")
        {
            transform.Rotate(0,0,0);
        }
        if (emgRawReader.readVal == "1")
        {
            transform.Rotate(1,0,0);
        }
        if (emgRawReader.readVal == "0")
        {
            transform.Rotate(-1,0,0);
        }
        if (emgRawReader.readVal == "3")
        {
            transform.Rotate(0,0,1);
        }
        if (emgRawReader.readVal == "4")
        {
            transform.Rotate(0,0,-1);
        }
        if (emgRawReader.readVal == "5")
        {
            transform.Rotate(0,1,0);
        }
        if (emgRawReader.readVal == "6")
        {
            transform.Rotate(0,-1,0);
        }
        }
}
