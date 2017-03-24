using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour {
    public float life = 1500.0f;
    public float thresh1 = 1000.0f;
    public float thresh2 = 500.0f;
    public bool isDead = false;
    public GameObject fire_large;
    public GameObject fire_middle;
    public GameObject fire_small;
    public GameObject fire_die;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if(life> thresh1)//(life*0.5f))
        {
            fire_large.SetActive(true);
            fire_middle.SetActive(false);
            fire_small.SetActive(false);
            fire_die.SetActive(false);
        }
        else if (life <= thresh1 && life > thresh2)//(life * 0.5f) && life > 0)
        {
            fire_large.SetActive(false);
            fire_middle.SetActive(true);
            fire_small.SetActive(false);
            fire_die.SetActive(false);
        }
        else if (life <= thresh1 && life > 0)//(life * 0.5f) && life > 0)
        {
            fire_large.SetActive(false);
            fire_middle.SetActive(false);
            fire_small.SetActive(true);
            fire_die.SetActive(false);
        }
        else
        {
            fire_large.SetActive(false);
            fire_middle.SetActive(false);
            fire_small.SetActive(false);
            fire_die.SetActive(true);
            isDead = true;
            GetComponent<BoxCollider>().enabled = false;
        }
	}

    public void Attacked()
    {
        //Debug.Log("FireEffect : Attacked!");
        if (life > 0)
            life-=1.0f;
    }
}
