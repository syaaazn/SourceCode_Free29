using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene_Config_MainMoDe_standby_solo : MonoBehaviour {

    public void SceneChange_Back(){
        SceneManager.LoadScene("Config_MainMode");
    }

    public void SceneChange_mian(){
        FadeManager.Instance.LoadScene("mian", 2.0f);
    }

    public void SceneChange_cointos(){
        FadeManager.Instance.LoadScene("cointos", 0.65f);
    }

}
