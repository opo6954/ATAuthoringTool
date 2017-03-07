using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class FPCUpdater : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.GetComponent<FirstPersonController>().transform.position = new Vector3(GameObject.Find("HoloLensCamera").transform.position.x, 0, GameObject.Find("HoloLensCamera").transform.position.z);
	}
}
