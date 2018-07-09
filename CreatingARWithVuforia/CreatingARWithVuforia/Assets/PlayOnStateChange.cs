using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnStateChange : MonoBehaviour {

    public AudioClip appearClip;
    public AudioClip disappearClip;
    private AudioSource source;


	// Use this for initialization
	void Awake () {
        source = GetComponent<AudioSource>();
        source.enabled = false;
	}
	
	public void Initialize()
    {
        source.enabled = true;
    } 

    public void PlayOnAppear()
    {
        source.clip = appearClip;
        source.Play();
    }

    public void PlayOnDisappear()
    {
        source.clip = disappearClip;
        source.Play();
    }
}
