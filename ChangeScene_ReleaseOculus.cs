using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene_ReleaseOculus : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SceneChange_Result(){
        //FadeManager.Instance.LoadScene("Title", 0.65f);
        Application.UnloadLevel("ReleaseOculus");
        Resources.UnloadUnusedAssets();

        Replay.ReplayManager.OneRelease = false;
        Replay.ReplayManager.OneResult = true;
    }
}
