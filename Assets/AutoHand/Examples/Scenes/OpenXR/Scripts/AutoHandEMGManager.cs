using System.Collections;
using System.Collections.Generic;
using Autohand;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Autohand.Demo {
    public class AutoHandEMGManager : MonoBehaviour
    {
        public InputActionProperty grabAction;
        public InputActionProperty releaseAction;
        private EMGRawReader emgRawReader;
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
            // Open Hand = Open AutoHand
            if (emgRawReader.readVal == "0")
            {
                releaseAction.action.Enable();
            }
            // Close Hand = Close AutoHand  
            if (emgRawReader.readVal == "1")
            {
                grabAction.action.Enable();
            }
            // Open Hand = Open AutoHand
            if (emgRawReader.readVal == "2")
            {
                releaseAction.action.Enable();
            }
            Color someColor = new Color(1-emgRawReader.velocity, emgRawReader.velocity, emgRawReader.velocity, 1f);
            rend.material.color = someColor;
        }
    }
}