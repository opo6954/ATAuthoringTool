using System.Collections;
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

    public int pauseCountOffset = 5;
    public Mode currentMode = Mode.NONE;
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
        currentMode = (Mode)newMode;
    }

    void GetGesture()
    {
        if (currentMode != Mode.NONE)
            return;
        if (myoInputManager.myoInputLeft.Cancel() || myoInputManager.myoInputRight.Cancel())
        {
            ShowMenu(false);
            pauseCountLeft = 0;
            pauseCountRight = 0;
            return;
        }

        // give a torrelance for gesture
        if (myoInputManager.myoInputLeft.Pause())
            pauseCountLeft = pauseCountOffset;
        if (myoInputManager.myoInputRight.Pause())
            pauseCountRight = pauseCountOffset;

        // show the menu for the gesture with two hands
        if (myoInputManager.myoInputLeft.Pause() || myoInputManager.myoInputRight.Pause())
        {
            ShowMenu(true);
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
        if (currentMode == Mode.EDITING)
        {
            floatingText.ShowText("Editing mode");
            envManager.EnableEditing();
            trainManager.StopTraining();
            //canvas.GetComponent<FloatingCanvas>().Deactivate();
            //canvas.GetComponent<FloatingCanvas>().ActivateObjectSelect();
        }
        else if(currentMode == Mode.TRAINING)
        {
            floatingText.ShowText("Training mode");
            envManager.DisableEditing();
            trainManager.StartTraining();
            //canvas.GetComponent<FloatingCanvas>().Deactivate();
        }
        else
        {
            envManager.DisableEditing();
            trainManager.StopTraining();
            canvas.GetComponent<FloatingCanvas>().ActivateModeSelect();
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetGesture();
        AdjustMode();
    }
}
