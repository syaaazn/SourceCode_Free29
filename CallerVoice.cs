using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CallerVoice : MonoBehaviour {

    public AudioSource CallerSource;
    public AudioClip[] CallerVoices_Right;
    public AudioClip[] CallerVoices_Left;
    public AudioClip[] CallerVoices_Rightangle;
    public AudioClip[] CallerVoices_Leftangle;
    public AudioClip[] CallerVoices_Motion;
    public AudioClip[] CallerVoices_Figure;

	void Start () {
		
	}
	
	void Update () {
       
	}

	private void OnTriggerEnter(Collider other){
		
        if(other.gameObject.name == "Player"){
            if(this.gameObject.name == "CallerArea_Right_30_9-10m"){
                StartCoroutine("CallerArea_Right_30_9_10m");
            }

            if (this.gameObject.name == "CallerArea_Right_45_8-9m")
            {
                StartCoroutine("CallerArea_Right_60_6_7m");
            }

            if (this.gameObject.name == "CallerArea_Right_60_6-7m"){
                StartCoroutine("CallerArea_Right_60_6_7m");
            }

            if (this.gameObject.name == "CallerArea_Left_30_9-10m"){
                StartCoroutine("CallerArea_Left_30_9_10m");
            }

            if (this.gameObject.name == "CallerArea_Left_60_6-7m"){
                StartCoroutine("CallerArea_Left_60_6_7m");
            }

        }
	}


    IEnumerator CallerArea_Right_30_9_10m(){
        CallerSource.PlayOneShot(CallerVoices_Figure[Random.Range(18, 23)]);
        yield return new WaitForSeconds(0.4f);
        CallerSource.PlayOneShot(CallerVoices_Rightangle[Random.Range(0, 3)]);
    }

    IEnumerator CallerArea_Right_60_6_7m(){
        CallerSource.PlayOneShot(CallerVoices_Figure[Random.Range(11, 14)]);
        yield return new WaitForSeconds(0.4f);
        CallerSource.PlayOneShot(CallerVoices_Rightangle[Random.Range(6, 9)]);
    }

    IEnumerator CallerArea_Right_45_8_9m(){
        CallerSource.PlayOneShot(CallerVoices_Figure[Random.Range(15, 20)]);
        yield return new WaitForSeconds(0.4f);
        CallerSource.PlayOneShot(CallerVoices_Rightangle[Random.Range(4, 5)]);
    }

    IEnumerator CallerArea_Left_30_9_10m(){
        CallerSource.PlayOneShot(CallerVoices_Figure[Random.Range(18, 23)]);
        yield return new WaitForSeconds(0.4f);
        CallerSource.PlayOneShot(CallerVoices_Leftangle[Random.Range(0, 1)]);
    }

    IEnumerator CallerArea_Left_60_6_7m(){
        CallerSource.PlayOneShot(CallerVoices_Figure[Random.Range(11, 14)]);
        yield return new WaitForSeconds(0.4f);
        CallerSource.PlayOneShot(CallerVoices_Leftangle[Random.Range(6, 7)]);
    }


}
