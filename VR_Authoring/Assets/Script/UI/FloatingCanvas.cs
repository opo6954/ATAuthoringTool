using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

public class FloatingCanvas : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void RepositionCanvas()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, (Camera.main.transform.position + Camera.main.transform.forward * 4.0f), 0.5f);
        //this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3.0f;
        this.transform.forward = Vector3.Lerp(this.transform.forward , Camera.main.transform.forward, 0.5f);
        
    }

}
