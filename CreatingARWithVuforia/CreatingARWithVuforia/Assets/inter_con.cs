using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inter_con : MonoBehaviour {
	
	public AudioClip[] aClips;
	public AudioSource myAudioSource;
	string btnNAme;
    public Animator anim;

    // Use this for initialization
    void Start () {
		myAudioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        
		if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began){
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit Hit;
			if(Physics.Raycast(ray, out Hit)){
				btnNAme = Hit.transform.name;
				switch (btnNAme){
                    case "Btn_Text1":
                        anim.Play("BunkerFire");
					 myAudioSource.clip = aClips[0];
					 myAudioSource.Play();
					 break;
                    default:
					 break;
				}
			}
		}
	}
}
