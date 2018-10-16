using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_Config_MainMode2 : MonoBehaviour
{

    public void SceneChange_Back(){
        FadeManager.Instance.LoadScene("Menu", 0.65f);
    }

    public void SceneChange_MainMode_standby(){
        FadeManager.Instance.LoadScene("mian", 2.0f);
    }

}

