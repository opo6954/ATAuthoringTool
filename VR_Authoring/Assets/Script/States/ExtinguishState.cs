using UnityEngine;
using System.Collections;
/*
 * 멈춰있는 상태에서 버튼을 누를 경우 완료
 * 필요한 Property:
 * 1. TaskName: 화재를 발견했습니다.
 * 2. ButtonInfo: X버튼을 눌러 다음 task를 수행하세요
 * 
 * 필요한 Object:
 * 필요없음
 * 
 * */
public class ExtinguishState : StateModuleTemplate {

    //GameObject backgroundUI;

	string button = "";
    GameObject extinguisherObject;


	public ExtinguishState(TaskModuleTemplate _myModule) : base(_myModule)
	{
		
	}
	 

	public override void setProperty (System.Collections.Generic.Dictionary<string, object> properties)
	{
		addProperty("Notice_Contents1", properties["Notice_Contents1"]);
        addProperty("Notice_Contents2", properties["Notice_Contents2"]);
        addProperty("Notice_Contents3", properties["Notice_Contents3"]);
        
	}

	public override void setObject (System.Collections.Generic.Dictionary<string, object> objects)
	{
		base.setObject (objects);
	}

    public override void Init()
    {
		myStateName = "화재 소화";

        base.Init();
        // WARNING : hard coding
        GameObject.Find("Extinguisher").GetComponent<Extinguisher>().SetHoseActive();
        extinguisherObject = GameObject.Find("FireExtinguisher");
        extinguisherObject.SetActive(false);
    }

    public override void Process()
    {
        base.Process();
        //backgroundUI.GetComponent<BackgroundForm>().toggleShownObject(BackgroundForm.BGPart.BG_BUTTONINFO, true);
    }

    public override bool Goal()
    {
        // WARNING : hard coding
        Debug.Log(GameObject.Find("M_Fire").GetComponent<FireEffect>().life);
        if(GameObject.Find("M_Fire").GetComponent<FireEffect>().isDead)
        {
            // WARNING : hard coding
            GameObject.Find("Extinguisher").GetComponent<Extinguisher>().SetHoseDeactive();
            extinguisherObject.SetActive(true);
            return true;
        }

        return base.Goal();
    }

    public override void Res()
    {
        base.Res();
        //lockFPSScreen(false);//unlock the screen
        //backgroundUI.GetComponent<BackgroundForm>().toggleShownObject(BackgroundForm.BGPart.BG_BUTTONINFO, false);
        //myUIInfo.GetComponent<DefaultForm>().toggleShownCurrTaskInfo(false);
        
    }

}
