using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectWrongSound : MonoBehaviour {
    public AudioClip[] sounds;
    private AudioSource audioSource;
    bool playOnce = true;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1;
        audioSource.dopplerLevel = 0;
        audioSource.loop = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayCorrectSound()
    {
        audioSource.clip = sounds[0];
        if (audioSource != null )
        {
            audioSource.Play();
        }
    }

    public void PlayWrongSound()
    {
        audioSource.clip = sounds[1];
        if (audioSource != null )
        {
            audioSource.Play();
        }
    }
}
