using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour {
    public ThalmicMyo myoRight;
    public GameObject spatialMapping;
    public GameObject canvas;

	// Use this for initialization
	void Start () {

        canvas.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(myoRight.pose == Thalmic.Myo.Pose.FingersSpread)
        {
            ShowMenu(true);
        }

        /*
        if (myoRight.pose == Thalmic.Myo.Pose.Fist)
        {
            ShowMenu(false);
        }
        */
    }

    public void ShowMenu(bool turnOn)
    {
        if (turnOn)
        {
            spatialMapping.SetActive(false);
            canvas.SetActive(true);
            canvas.GetComponent<FloatingCanvas>().RepositionCanvas();
        }
        else
        {
            spatialMapping.SetActive(true);
            canvas.SetActive(false);
        }
    }
}
