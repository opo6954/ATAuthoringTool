using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

public class FloatingCanvas : MonoBehaviour {
    public GazeChecker gazeChecker;
    public GazeChecker btn1Checker;
    public GazeChecker btn2Checker;
    public GazeChecker btn3Checker;


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        /*
        if (!gazeChecker.gazing && !btn1Checker.gazing && !btn2Checker.gazing && !btn3Checker.gazing)
        {
            Debug.Log("!!!");
            
        }
        else
        {
            Debug.Log("???");
        }
        */
	}

    public void RepositionCanvas()
    {
        this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 10.0f;
        this.transform.forward = Camera.main.transform.forward;
        //gazeChecker.gazing = true;
    }

}
