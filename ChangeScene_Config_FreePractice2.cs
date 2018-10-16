using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_Config_FreePractice2 : MonoBehaviour
{

    public void SceneChange_Back()
    {
        SceneManager.LoadScene("Config_PracticeMode");
    }

    public void SceneChange_Config_Practice_standby()
    {
        SceneManager.LoadScene("Config_FreePractice");
    }

}