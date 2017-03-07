using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class EnvironmentManager : MonoBehaviour {
    public MyoInputManager myoInputManager;
    public GameObject spatialMapping;
    public GameObject canvas;
    public GameObject interactableObjects;
    public bool isEditing;

	// Use this for initialization
	void Start () {
        canvas.GetComponent<FloatingCanvas>().Deactivate();
        if (!isEditing)
            DisableEditing();
        else
            EnableEditing();
    }
	
	// Update is called once per frame
	void Update () {
        if (isEditing)
        {
            if (myoInputManager.myoInputLeft.Pause() || myoInputManager.myoInputRight.Pause())
            {
                ShowMenu(true);
            }

            if (myoInputManager.myoInputLeft.Cancel() || myoInputManager.myoInputRight.Cancel())
            {
                this.transform.parent.GetComponent<ModeSelector>().SetMode(ModeSelector.Mode.NONE);
                this.transform.parent.GetComponent<ModeSelector>().ShowMenu(true);
            }
        }
        else
        {
            canvas.GetComponent<FloatingCanvas>().Deactivate();
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
            canvas.GetComponent<FloatingCanvas>().ActivateObjectSelect();
            canvas.GetComponent<FloatingCanvas>().RepositionCanvas();            
        }
        else
        {
            spatialMapping.SetActive(true);
            canvas.GetComponent<FloatingCanvas>().Deactivate();
        }
    }
}
