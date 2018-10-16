using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSideFencesSound : MonoBehaviour {

    public AudioSource sound_bound;
    public AudioClip[] sound;
    //public float volume;

	// Use this for initialization
	void Start () {
        sound_bound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnCollisionEnter(Collision hit)
    {

        if (hit.gameObject.tag == "Player")
        {
            sound_bound.PlayOneShot(sound[0], 0.1f);
            sound_bound.PlayOneShot(sound[1], 0.8f);
        }
       
        if (hit.gameObject.tag == "SoccerBall")
        {
            sound_bound.PlayOneShot(sound[2]);
        }

       

    }

}
