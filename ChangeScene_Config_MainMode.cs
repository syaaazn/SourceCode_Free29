using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_Config_MainMode : MonoBehaviour
{

    public void SceneChange_Back()
    {
        FadeManager.Instance.LoadScene("Menu", 0.65f);
    }


    public void SceneChange_Solo()
    {
        SceneManager.LoadScene("Config_MainMode_standby_Solo");
    }

    public void SceneChange_Two()
    {
        SceneManager.LoadScene("");
    }

}

