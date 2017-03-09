using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour {
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public FireEffect fe;
    // Use this for initialization
    void Start () {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnParticleCollision(GameObject other)
    {
        //if(other.transform.name== "M_Fire")
            fe.Attacked();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        //if (other.transform.name == "M_Fire")
        fe.Attacked();
    }
}
