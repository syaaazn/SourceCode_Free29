using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene_Menu : MonoBehaviour{

    public Text DescriptionText;

	private void Start(){
        Time.timeScale = 1.0f;
        DescriptionText.text = "モードを選択してください。";
	}

	public void SceneChange_Back()
    {
        FadeManager.Instance.LoadScene("Title", 0.65f);
    }


    public void SceneChange_MainMode()
    {
        FadeManager.Instance.LoadScene("Config_MainMode", 0.65f);
    }

    public void SceneChange_PracticeMode()
    {
        FadeManager.Instance.LoadScene("Config_PracticeMode", 0.65f);

    }

    public void PointerEnter_Main(){
        DescriptionText.text = "このモードは実戦に基づき、実際に目隠しをして5対5の人数で試合をするモードです。";
    }

    public void PointerEnter_Practice(){
        DescriptionText.text = "このモードはMainModeでより良いプレイをするための練習をするモードです。";
    }

    public void PointerExit(){
        DescriptionText.text = "モードを選択してください。";
    }

}