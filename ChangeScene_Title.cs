using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_Title : MonoBehaviour {
    
	void Update () {

        if (Input.anyKeyDown){

            FadeManager.Instance.LoadScene("Menu", 0.65f);
        }
	}


}
