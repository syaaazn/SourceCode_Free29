using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_Config_Tutorial : MonoBehaviour{

    public void SceneChange_Back(){
        FadeManager.Instance.LoadScene("Config_PracticeMode",0.65f);
    }

    public void SceneChange_MainMode_standby(){
        SceneManager.LoadScene("Config_MainMode_standby_Solo");
    }

}

