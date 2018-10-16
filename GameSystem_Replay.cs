using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem_Replay : MonoBehaviour{

    private bool isReplaying = false;
    public bool IsReplaying { get { return isReplaying; } set { isReplaying = value; } }
	
	void Start () {

        //Replay.ReplayManager.

        Invoke("Run", 4.0f);
	}
	
	
    void Run(){

        this.IsReplaying = true;

    }

	void Update () {
		
	}


}
