﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class ModeSelector : MonoBehaviour {
    public enum Mode { NONE = 0, EDITING = 1, TRAINING = 2, TRAINING_TASK = 3}
    public TimedBillText floatingText;
    public MyoInputManager myoInputManager;
    public GameObject canvas;
    public GameObject spatialMapping;
    public EnvironmentManager envManager;
    public TrainingManager trainManager;

    bool isEnabled = false;
    public int pauseCountOffset = 5;
    public Mode currentMode = Mode.NONE;
    public Mode previousMode = Mode.NONE;
    int pauseCountLeft;
    int pauseCountRight;

    // Use this for initialization
    void Start () {
        currentMode = Mode.NONE;
	}

    public void SetMode(Mode newMode)
    {
        currentMode = newMode;
    }

    public void SetMode(int newMode)
    {
        Debug.Log("ModeSelector : mode - " + newMode);
        currentMode = (Mode)newMode;
    }

    void GetGesture()
    {
        if (myoInputManager.myoInputLeft.Next() || myoInputManager.myoInputRight.Next())
        {
            currentMode = Mode.TRAINING;
            isEnabled = false;
            /*
            ShowMenu(false);
            pauseCountLeft = 0;
            pauseCountRight = 0;
            return;
            */
        }
        if (currentMode != Mode.NONE)
            return;
        
        // give a torrelance for gesture
        if (myoInputManager.myoInputLeft.Pause())
            pauseCountLeft = pauseCountOffset;
        if (myoInputManager.myoInputRight.Pause())
            pauseCountRight = pauseCountOffset;

        // show the menu for the gesture with two hands
        if (myoInputManager.myoInputLeft.Pause() || myoInputManager.myoInputRight.Pause())
        {
            //ShowMenu(true);
            currentMode = Mode.EDITING;
            isEnabled = false;
        }
    }
	
    public void ShowMenu(bool turnOn)
    {
        if (turnOn)
        {
            canvas.GetComponent<FloatingCanvas>().ActivateModeSelect();
            canvas.GetComponent<FloatingCanvas>().RepositionCanvas();
            spatialMapping.SetActive(false);
        }
        else
        {
            canvas.GetComponent<FloatingCanvas>().Deactivate();
            spatialMapping.SetActive(true);
        }
    }

    void AdjustMode()
    {
        if (currentMode == Mode.EDITING && previousMode != Mode.EDITING)
        {
            floatingText.ShowText("Editing mode");
            envManager.EnableEditing();
            trainManager.StopTraining();
            //canvas.GetComponent<FloatingCanvas>().Deactivate();
            //canvas.GetComponent<FloatingCanvas>().ActivateObjectSelect();
        }
        else if(currentMode == Mode.TRAINING && previousMode != Mode.TRAINING)
        {
            floatingText.ShowText("Training mode");
            envManager.DisableEditing();
            trainManager.StartTraining();
            //canvas.GetComponent<FloatingCanvas>().Deactivate();
        }
        else if(currentMode == Mode.NONE && previousMode != Mode.NONE)
        {
            envManager.DisableEditing();
            trainManager.StopTraining();
            canvas.GetComponent<FloatingCanvas>().ActivateModeSelect();
        }
        previousMode = currentMode;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            GetGesture();
        }
        AdjustMode();
    }
}
