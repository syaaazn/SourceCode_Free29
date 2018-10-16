using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene_Main : MonoBehaviour{
    
    public GameObject attention_retire;
    public GameObject attention_retry;
    public GameObject ConfigPanel;
    public GameObject ConfigPanel_Volume;

    public GameObject ConfigPanel_Color;
    public GameObject ConfigPanel_HowtoControl;
    public GameObject ConfigPanel_About;

    public GameObject Uniduino;

    public GameObject ReplayEndPanel;

    private static bool isEnding_Replay;
    public static bool IsEnding_Replay{ get { return isEnding_Replay; } set { isEnding_Replay = value; }}

	private void Start(){

       // DontDestroyOnLoad(Uniduino);

        isEnding_Replay = false;
        ReplayEndPanel.SetActive(false);

        attention_retire.SetActive(false);
        attention_retry.SetActive(false);

        ConfigPanel.SetActive(false);
        ConfigPanel_Volume.SetActive(false);
        ConfigPanel_Color.SetActive(false);
        ConfigPanel_About.SetActive(false);

	}

	public void SceneChange_Menu(){
        Time.timeScale = 1;
        isEnding_Replay = true;
        FadeManager.Instance.LoadScene("Menu", 1.0f);
    }

    public void SceneChange_Retry(){
        Time.timeScale = 1;
        FadeManager.Instance.LoadScene("mian", 1.0f);
    }
    public void SceneChange_Retry_Replay(){
        isEnding_Replay = true;
        Time.timeScale = 1;
        FadeManager.Instance.LoadScene("cointos", 1.0f);
    }

    public void SceneChange_Config_PracticeMode(){
        Time.timeScale = 1;
        isEnding_Replay = true;
        FadeManager.Instance.LoadScene("Config_PracticeMode", 1.0f);
    }

    public void SceneChange_Retry_FreePractice(){
        Time.timeScale = 1;
        FadeManager.Instance.LoadScene("FreePractice", 1.0f);
    }

    public void SceneChange_Retry_Replay_FreePractice(){
        isEnding_Replay = true;
        Time.timeScale = 1;
        FadeManager.Instance.LoadScene("FreePractice", 1.0f);
    }

    public void SetActiveAttentionPanel_Retire_false(){
        attention_retire.SetActive(false);
    }

    public void SetActiveAttentionPanel_Retire_true(){
        attention_retire.SetActive(true);
    }

    public void SetActiveAttentionPanel_Retry_false(){
        attention_retry.SetActive(false);
    }

    public void SetActiveAttentionPanel_Retry_true(){
        attention_retry.SetActive(true);
    }




    public void SetActiveConfigPanel_true(){

        ConfigPanel.SetActive(true);
    }
    public void SetActiveConfigPanel_false(){

        ConfigPanel.SetActive(false);
    }

    public void SetActiveConfigPanel_Volume_true(){

        ConfigPanel_Volume.SetActive(true);
    }
    public void SetActiveConfigPanel_Volume_false(){

        ConfigPanel_Volume.SetActive(false);
    }

    public void SetActiveConfigPanel_Color_true(){

        ConfigPanel_Color.SetActive(true);
    }
    public void SetActiveConfigPanel_Color_false(){

        ConfigPanel_Color.SetActive(false);
    }

    public void SetActiveConfigPanel_HowtoControl_true()
    {

        ConfigPanel_HowtoControl.SetActive(true);
    }
    public void SetActiveConfigPanel_HowtoControl_false()
    {

        ConfigPanel_HowtoControl.SetActive(false);
    }

    public void SetActiveConfigPanel_About_true()
    {

        ConfigPanel_About.SetActive(true);
    }
    public void SetActiveConfigPanel_About_false()
    {

        ConfigPanel_About.SetActive(false);
    }


    public void SetActiveEndReplay_true(){
        isEnding_Replay = true;
        ReplayEndPanel.SetActive(true);
    }

    public void SetActiveEndReplay_false(){
        isEnding_Replay = false;
        ReplayEndPanel.SetActive(false);
    }

	
}