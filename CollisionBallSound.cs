using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBallSound : MonoBehaviour {

    public AudioSource sound_bound;
    public AudioClip[] sound;
    //public float volume;


	void Start () {
        sound_bound = GetComponent<AudioSource>();

	}
	
	void Update () {

        //sound_bound.PlayOneShot(sound[0]);
       
	}


    void OnCollisionEnter(Collision hit){

        if (hit.gameObject.tag == "SideFences"){
            sound_bound.PlayOneShot(sound[6],0.5f);
            sound_bound.PlayOneShot(sound[2]);

      //      Debug.Log("Hit_SideFences");
        }

        if (hit.gameObject.tag == "Floar"){
            sound_bound.PlayOneShot(sound[2]);
            sound_bound.PlayOneShot(sound[3],0.007f);
//        Debug.Log("Hit_Floar");
        }

        if (hit.gameObject.tag == "Goals"){
            sound_bound.PlayOneShot(sound[4]);
     //       Debug.Log("Hit_Goals");
        }



    }
    /*
     void OnCollisionStay(Collision stay){
        if (stay.gameObject.tag == "SoccerPlayer"){
            sound_bound.loop = true;

            sound_bound.PlayOneShot(sound[5],0.05f);
            Debug.Log("Hit_SoccerPlayer");
        }

	}
*/   
   
}
