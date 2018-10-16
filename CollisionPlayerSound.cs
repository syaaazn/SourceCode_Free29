using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionPlayerSound : MonoBehaviour
{

    public AudioSource sound_bound;
    public AudioClip[] sound;
    //public float volume;



    void Start(){
        sound_bound = GetComponent<AudioSource>();

    }

    void Update(){

    }


    void OnCollisionEnter(Collision hit){

        if (hit.gameObject.tag == "SideFences" ||hit.gameObject.tag == "Goals" )
        {
            sound_bound.PlayOneShot(sound[3],0.1f);
            sound_bound.PlayOneShot(sound[4], 0.8f);
        }
        /*
        if (hit.gameObject.tag == "Floar")
        {
            sound_bound.PlayOneShot(sound[0]);
            sound_bound.PlayOneShot(sound[1], 0.007f);
        }
*/
        if (hit.gameObject.tag == "SoccerBall")
        {
            sound_bound.PlayOneShot(sound[2]);
        }

    }

    /*
     void OnCollisionStay(Collision stay){
        if (stay.gameObject.tag == "Floar")
        {

            StartCoroutine(Step());
        }

    }*/

    /*
    IEnumerator Step(){
        
   
        //sound_bound.loop = true;

        //sound_bound.PlayOneShot(sound[5],0.05f);

        sound_bound.PlayOneShot(sound[0], 0.050f);
        sound_bound.PlayOneShot(sound[1], 0.007f);

        Debug.Log("Hit_SoccerPlayer");
        yield return new WaitForSeconds(5.0f);
    }*/
  

}
