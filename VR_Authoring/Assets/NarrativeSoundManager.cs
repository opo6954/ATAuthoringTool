using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeSoundManager : MonoBehaviour {
    public AudioClip[] sounds;
    public int currentSoundIdx;
    private AudioSource audioSource;
    bool playOnce = true;
    // Use this for initialization
    void Start () {
        currentSoundIdx = 0;
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

    public void Play()
    {
        audioSource.clip = sounds[currentSoundIdx];
        if (audioSource != null && !audioSource.isPlaying && playOnce)
        {
            audioSource.Play();
            playOnce = false;
        }
    }

    public void MoveNextSound()
    {
        currentSoundIdx++;
        playOnce = true;
    }
}
