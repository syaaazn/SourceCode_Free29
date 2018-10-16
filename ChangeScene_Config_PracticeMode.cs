using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene_Config_PracticeMode : MonoBehaviour {

    public Text DescriptionText;

	public void Start(){
        DescriptionText.text = "モードを選択してください。";
	}

	public void SceneChange_Back(){
        FadeManager.Instance.LoadScene("Menu", 0.65f);
    }

    public void SceneChange_Tutorial(){
        FadeManager.Instance.LoadScene("Config_Tutorial", 0.65f);
    }

    public void SceneChange_FreePractice(){
        FadeManager.Instance.LoadScene("Config_PracticeMode_FreePractice_standby", 0.65f);
    }

    public void PointerEnter_Tutorial(){
        DescriptionText.text = "このゲームの遊び方を学べます。";
    }

    public void PointerEnter_FreePractice(){
        DescriptionText.text = "時間制限なし、1対1で自由に練習できます。";
    }

    public void PointerExit(){
        DescriptionText.text = "モードを選択してください。";
    }

}
