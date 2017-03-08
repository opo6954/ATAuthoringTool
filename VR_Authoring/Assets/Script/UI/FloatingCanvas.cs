using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;

public class FloatingCanvas : MonoBehaviour {
    public GameObject objectSelectUI;
    public GameObject modeSelectUI;
    public bool activated = false;
    Vector3 targetPosition;
    Vector3 targetRotation;

    // Use this for initialization
    void Start () {
        Deactivate();
    }

    public void Deactivate()
    {
        objectSelectUI.SetActive(false);
        modeSelectUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, 0.2f);
        //this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 3.0f;
        this.transform.forward = Vector3.Lerp(this.transform.forward, targetRotation, 0.2f);
    }

    public void ActivateObjectSelect()
    {
        objectSelectUI.SetActive(true);
        modeSelectUI.SetActive(false);
    }

    public void ActivateModeSelect()
    {
        objectSelectUI.SetActive(false);
        modeSelectUI.SetActive(true);
    }

    public void RepositionCanvas()
    {
        targetPosition = (Camera.main.transform.position + Camera.main.transform.forward * 2.0f);
        targetRotation = Camera.main.transform.forward;
    }

}
