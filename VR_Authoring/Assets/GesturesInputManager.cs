using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class GesturesInputManager : MonoBehaviour {
    public GesturesInput hololensInput;
    public int tapCountOffset = 10;
    int tapCount;
    bool holdState = false;
	// Use this for initialization
	void Start () {
        tapCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (hololensInput.currentGesture == GesturesInput.Gesture.TAP && !holdState)
        {
            holdState = true;
            tapCount = tapCountOffset;
        }
        if (tapCount > 0 && holdState)
            tapCount--;
        if (tapCount == 0) {
            ResetGesture();
        }        
	}

    public void ResetGesture()
    {
        hololensInput.currentGesture = GesturesInput.Gesture.NONE;
        holdState = false;
    }
}
