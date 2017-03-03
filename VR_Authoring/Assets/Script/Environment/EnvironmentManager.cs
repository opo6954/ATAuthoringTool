using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class EnvironmentManager : MonoBehaviour {
    public MyoInputManager myoInputManager;
    public GameObject spatialMapping;
    public GameObject canvas;
    public GameObject interactableObjects;
    public bool isEditing=false;

	// Use this for initialization
	void Start () {
        canvas.SetActive(false);
        DisableEditing();
    }
	
	// Update is called once per frame
	void Update () {
        if (isEditing)
        {
            if (myoInputManager.myoInput1.Pause() || myoInputManager.myoInput2.Pause())
            {
                ShowMenu(true);
            }

            if (myoInputManager.myoInput1.Cancel() || myoInputManager.myoInput2.Cancel())
            {
                ShowMenu(false);
            }
        }
    }

    public void EnableEditing()
    {
        isEditing = true;
        foreach (Transform child in interactableObjects.transform)
        {
            child.GetComponent<TapToPlace>().IsEditing = true;
        }
    }

    public void DisableEditing()
    {
        isEditing = false;
        foreach (Transform child in interactableObjects.transform)
        {
            child.GetComponent<TapToPlace>().IsEditing = false;
        }
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
