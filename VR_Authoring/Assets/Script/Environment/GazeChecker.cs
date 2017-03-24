using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class GazeChecker : MonoBehaviour, IFocusable, IInputClickHandler
{
    public bool gazing = false;

    // Use this for initialization
    void Start () {
		
	}
	

    public void OnFocusEnter()
    {
        gazing = true;
    }

    public void OnFocusExit()
    {
        gazing = false;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        
    }
}
