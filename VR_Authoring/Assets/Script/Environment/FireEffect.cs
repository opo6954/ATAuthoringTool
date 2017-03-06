using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour {
    public int life = 100;

    public GameObject fire_large;
    public GameObject fire_middle;
    public GameObject fire_die;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if(life>50)
        {
            fire_large.SetActive(true);
            fire_middle.SetActive(false);
            fire_die.SetActive(false);
        }
        else if (life > 0)
        {
            fire_large.SetActive(false);
            fire_middle.SetActive(true);
            fire_die.SetActive(false);
        }
        else
        {
            fire_large.SetActive(false);
            fire_middle.SetActive(false);
            fire_die.SetActive(true);
        }
	}

    public void Attacked()
    {
        if (life > 0)
            life--;
    }
}
