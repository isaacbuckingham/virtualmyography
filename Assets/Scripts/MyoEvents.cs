using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyoEvents : MonoBehaviour
{
    private EMGRawReader emgRawReader;
    private Renderer rend;

    public UnityEvent Gesture0;
    public UnityEvent Gesture1;
    public UnityEvent Gesture2;
    public UnityEvent Gesture3;
    public UnityEvent Gesture4;
    public UnityEvent Gesture5;
    public UnityEvent Gesture6;
    public UnityEvent PressK;

    // Start is called before the first frame update
    void Start()
    {
        emgRawReader = FindObjectOfType<EMGRawReader>();
        rend = GetComponent<Renderer>();  
    }

    // Update is called once per frame
    void Update()
    {
        switch (emgRawReader.readVal)
        {
            case "0":
                Gesture0?.Invoke();
                break;
            case "1":
                Gesture1?.Invoke();
                break;
            case "2":
                Gesture2?.Invoke();
                break;
            case "3":
                Gesture3?.Invoke();
                break;
            case "4":
                Gesture4?.Invoke();
                break;
            case "5":
                Gesture5?.Invoke();
                break;
            case "6":
                Gesture6?.Invoke();
                break;
            default:
                break;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            PressK?.Invoke();
            Debug.Log("K Key Pressed");
        }
    }
}
