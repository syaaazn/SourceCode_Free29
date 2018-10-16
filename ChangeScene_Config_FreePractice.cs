using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_Config_FreePractice : MonoBehaviour
{

    public void SceneChange_Back(){
        SceneManager.LoadScene("Config_PracticeMode_FreePractice_standby");
    }

    public void SceneChange_FreePractice_standby(){
        FadeManager.Instance.LoadScene("FreePractice",2.0f);
    }

}

