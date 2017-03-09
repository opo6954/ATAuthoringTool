using UnityEngine;
using System.Collections;

/*
 * method state, 3D 물체의 각 부분을 인식하고 관련 영상 재생하기
 * 3D object를 확인하고 선택하는 효과는 일단 바꿀 수 없게 하는데 바꾸는 게 저작도구에는 넣어야 하지 않을까?
 * */



public class MethodLearnState : StateModuleTemplate {

    //아마 추가적인 information이 들어가겠지?
    GameObject fireExtinguisherModel;
    TimedBillText floatingText;
    int currentState = 0;
    bool completed = false;

	float originHeight;

	float objOffsetRot=70.0f;

	Transform[] objParts;

	//GameObject backgroundUI;//background UI instance
	GameObject cloneObj;//clone obj

	Material[] partObjectify;
	Material partSelectObjectify;

	string selectButton="";
	string skipButton="";

	int selectIdx=-1;
	int currIdxOrder=0;

	int[] trueAns;

	bool isPlayingVideo = false;



	 




	public MethodLearnState(TaskModuleTemplate _myModule) : base(_myModule)
	{

	}

	public override void setProperty (System.Collections.Generic.Dictionary<string, object> properties)
	{
        addProperty("Notice_Contents", properties["Notice_Contents"]);
        addProperty ("PartCount", properties ["PartCount"]);//활성화된 부분 개수
		addProperty("isVideo", properties["isVideo"]);//Video 여부
		addProperty ("VideoName", properties ["VideoName"]);//video 이름
		addProperty("PartAnswer", properties["PartAnswer"]);//part 선택 순서


		addProperty ("Select_Button_Info", properties ["Select_Button_Info"]);
		//raycasting으로 highligh된 부분 선택하는 키 && 영상 재생시 일시정지 및 다시 재생 버튼 및 다시 재생 버튼(재생 다 끝날시)
		addProperty ("Skip_Button_Info", properties ["Skip_Button_Info"]);//영상 skip 버튼



		selectButton = getProperty<string> ("Select_Button_Info");
		skipButton = getProperty<string> ("Skip_Button_Info");
		trueAns = getProperty<int[]> ("PartAnswer");

		 


	}

	public override void setObject (System.Collections.Generic.Dictionary<string, object> objects)
	{
		addObject ("Interaction_to_Object", objects ["Interaction_to_Object"]);
	}








	///////////////////////////////INITIALIZE...
	/// 
	public void setInitScaleObj(GameObject cloneObj)
	{
		//scale 조절
		Vector3 myScale = cloneObj.transform.localScale;
		myScale = myScale * 1.5f;
		cloneObj.transform.localScale = myScale;
	}

	public void setInitPosObj(GameObject cloneObj)
	{
		//위치 조절
        /*
		Ray ray = myPlayerInfo.getCamera ().ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2,0));
		//Debug.DrawRay (ray.origin, ray.direction * 1000, Color.yellow);

		Vector3 objPosition = new Vector3 ();
		objPosition = ray.origin +  ray.direction * 2.0f;

		objPosition.y = objPosition.y - cloneObj.GetComponent<MeshFilter> ().mesh.bounds.max.z / 100.0f;

		cloneObj.transform.localPosition = objPosition;
        */
	}

	public void setInitRotObj(GameObject cloneObj)
	{ 
		//각도 조절
		Quaternion q = cloneObj.transform.localRotation;
		Vector3 myEuler = myPosition.rotation.eulerAngles;
		Vector3 objEuler = new Vector3 ();
		objEuler.x = cloneObj.transform.rotation.eulerAngles.x;
		objEuler.y = myEuler.y + objOffsetRot;
		objEuler.z = myEuler.z;

		cloneObj.transform.localRotation = Quaternion.Euler(objEuler);
	}



	public override void Init ()
	{
		myStateName = "Method 숙지 - 3D 모델 인식 및 영상 교육";
        Debug.Log(myStateName);

        base.Init ();
        // WARNING : hard coding
        fireExtinguisherModel = GameObject.Find("M_FireExtinguisher");
        enableAllParts();
        floatingText = GameObject.Find("FloatingUI_Timed").GetComponent<TimedBillText>();

	}

    void enableAllParts()
    {
        foreach (Transform child in fireExtinguisherModel.transform)
        {
            child.GetComponent<GazeMagnifier>().isEnabled = true;
        }
    }

	/////////////////////////////////////////PROCESS
	public void playVideo()
	{
		//myUIInfo.GetComponent<MethodForm> ().toggleShownVideoInfo (true);
		//myUIInfo.GetComponent<MethodForm> ().playVideo (selectIdx);
		isPlayingVideo = true;
	}

	public void hitTest()
	{
        /*
		Ray ray = myPlayerInfo.getCamera ().ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
		RaycastHit hitObj;

		var layerMask = 1 << 8;

		if (Physics.Raycast (ray, out hitObj, 10.0f,layerMask)) {

			for (int i = 0; i < objParts.Length; i++) {
				if (hitObj.transform.name == objParts [i].name) {
					//objParts[i].GetComponent<MeshRenderer> ().materials [0] = partSelectObjectify;
					hitObj.transform.GetComponent<MeshRenderer> ().material = partSelectObjectify;

					selectIdx = i;

				} else {
					objParts [i].GetComponent<MeshRenderer> ().material = partObjectify [i];
				}
			}			
		}
        */
	}

    public void goWrong()
    {
        GameObject.Find("FloatingUI_Timed").GetComponent<TimedBillText>().ShowText("틀렸습니다. 처음부터 다시 하세요.");
        currentState = 0;
        enableAllParts();
        playWrongSound();
    }

    void playCorrectSound()
    {
        Debug.Log("Correct!");
        GameObject.Find("CorrectWrongSound").GetComponent<CorrectWrongSound>().PlayCorrectSound();
    }

    void playWrongSound()
    {
        Debug.Log("Wrong!");
        GameObject.Find("CorrectWrongSound").GetComponent<CorrectWrongSound>().PlayWrongSound();
    }

	public void selectTest()
    {
        if (currentState >= 3)
        {
            completed = true;
        }
        //if(isKeyDown(selectButton) && isPlayingVideo == false)
        if (isHoloGestureTapped())
        {
            // WARNING : hard coding
            if (currentState == 0) {
                if (GameObject.Find("MP_ReleasePin").GetComponent<GazeMagnifier>().isGazed)
                {
                    currentState++;
                    GameObject.Find("MP_ReleasePin").GetComponent<GazeMagnifier>().isEnabled = false;
                    playCorrectSound();
                    InvalidateHoloGesture();
                }
                else
                {
                    goWrong();
                }
            }
            else if(currentState == 1)
            {
                if (GameObject.Find("MP_Hose").GetComponent<GazeMagnifier>().isGazed)
                {
                    currentState++;
                    GameObject.Find("MP_Hose").GetComponent<GazeMagnifier>().isEnabled = false;
                    playCorrectSound();
                    InvalidateHoloGesture();
                }
                else
                {
                    goWrong();
                }
            }
            else if(currentState == 2)
            {
                if (GameObject.Find("MP_Handle").GetComponent<GazeMagnifier>().isGazed)
                {
                    currentState++;
                    GameObject.Find("MP_Handle").GetComponent<GazeMagnifier>().isEnabled = false;
                    playCorrectSound();
                    InvalidateHoloGesture();
                }
                else
                {
                    goWrong();
                }
            }
		}
	}


    public override void Process ()
	{
        
		selectTest ();





		base.Process ();
	}

	public override bool Goal ()
	{
        if (completed)
        {
            GameObject.Find("NarrativeSoundManager").GetComponent<NarrativeSoundManager>().MoveNextSound();
            return true;
        }
        else
            return false;
        
        
	}

	public override void Res ()
	{
		//cloneObj.SetActive (false);
		//getObject<GameObject> ("Interaction_to_Object").SetActive (true);
        
		base.Res ();
	}
	
}
