using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingManager : MonoBehaviour {
    public ScenarioController sc;
    public GameObject floatingText;
    public bool isTraining;
    public Extinguisher extingushing;
    ScenarioModuleTemplate currentScenario;
    public DebugAuthoringTools dat;
    int scenarioIndex = 0;

	// Use this for initialization
	void Start () {
        isTraining = false;
        dat.Init();
    }

    public void StartTraining()
    {
        isTraining = true;
        //extingushing.SetHoseActive();
    }

    public void StopTraining()
    {
        isTraining = false;
        //extingushing.SetHoseDeactive();
    }
	
	// Update is called once per frame
	void Update () {
        //if (!isTraining)
            //return;
        if (sc.scenarioSeq.Count > 0)
        {
            currentScenario = sc.scenarioSeq[scenarioIndex];

            //if taskStart function is already called that contains building of state list...
            if (sc.currTaskExecute.isTaskDoing == true)
            {
                if (sc.currTaskExecute.stateIdx < sc.currTaskExecute.myStateList.Count && sc.currTaskExecute.myStateList[sc.currTaskExecute.stateIdx].isContainProperty("Patrol_Contents"))
                {
                    floatingText.GetComponent<TimedBillText>().ShowText(sc.currTaskExecute.myStateList[sc.currTaskExecute.stateIdx].getProperty<string>("Patrol_Contents"));
                }
                else if (sc.currTaskExecute.stateIdx < sc.currTaskExecute.myStateList.Count && sc.currTaskExecute.myStateList[sc.currTaskExecute.stateIdx].isContainProperty("Notice_Contents"))
                {
                    floatingText.GetComponent<TimedBillText>().ShowText(sc.currTaskExecute.myStateList[sc.currTaskExecute.stateIdx].getProperty<string>("Notice_Contents"));
                }
            }
        }        
	}
}
