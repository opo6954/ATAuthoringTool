using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

public class FloatingCanvas : MonoBehaviour {
    public GameObject objectSelectUI;
    public GameObject modeSelectUI;
    Vector3 targetPosition;
    Vector3 targetRotation;

    float hideOffset = -5;
    
    int showingMode = 0;

    // Use this for initialization
    void Start () {
        Deactivate();
    }

    public void Deactivate()
    {
        objectSelectUI.SetActive(false);
        //modeSelectUI.SetActive(false);

        //RenderSelect(objectSelectUI, false);
        //RenderSelect(modeSelectUI, false);
        //showingMode = 0;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, 0.2f);
        //this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3.0f;
        this.transform.forward = Vector3.Lerp(this.transform.forward, targetRotation, 0.2f);
        
        /*
        if (showingMode==1)
        {
            modeSelectUI.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, 0), 0.2f);
            objectSelectUI.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, hideOffset), 0.2f);
        }
        else if(showingMode==2)
        {
            modeSelectUI.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, hideOffset), 0.2f);
            objectSelectUI.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, 0), 0.2f);
        }
        else
        {
            modeSelectUI.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, hideOffset), 0.2f);
            objectSelectUI.transform.position = Vector3.Lerp(this.transform.position, new Vector3(this.transform.position.x, this.transform.position.y, hideOffset), 0.2f);
        }
        */
    }

    public void ActivateObjectSelect()
    {
        objectSelectUI.SetActive(true);
        //modeSelectUI.SetActive(false);
        showingMode = 2;
        //RenderSelect(objectSelectUI, true);
        //RenderSelect(modeSelectUI, false);
    }

    public void RenderSelect(GameObject obj, bool turnOn)
    {
        foreach(Transform child in obj.transform)
        {
            child.gameObject.SetActive(turnOn);
        }
    }

    public void ActivateModeSelect()
    {
        //objectSelectUI.SetActive(false);
        //modeSelectUI.SetActive(true);
        //showingMode = 1;
        //RenderSelect(objectSelectUI, false);
        //RenderSelect(modeSelectUI, true);
    }

    public void RepositionCanvas()
    {
        targetPosition = (Camera.main.transform.position + Camera.main.transform.forward * 2.0f);
        targetRotation = Camera.main.transform.forward;
    }

}
