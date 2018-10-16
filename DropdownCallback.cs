using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropdownCallback : MonoBehaviour{

    private int[] dropdown_num;
    private static int playTime = 0;
    private static int Team1_member = 0;
    private static int Team2_member = 1;

    public GameObject[] Team1_panel;
    public GameObject[] Team2_panel;

    public static int PlayTime{ get { return playTime; } set { playTime = value; }}
    public static int Team1_Member{ get { return Team1_member; } set { Team1_member = value; }}
    public static int Team2_Member { get { return Team2_member; } set { Team2_member = value; } }

    private void Start(){

        if(SceneManager.GetActiveScene().name == "Config_MainMode_standby_Solo"){
            Team1_member = 3;
            Team2_member = 4;
            playTime = 5;
            this.gameObject.GetComponent<Dropdown>().value = 2;
        }

        if (SceneManager.GetActiveScene().name == "Config_PracticeMode_FreePractice_standby"){
            // this.gameObject.GetComponent<Dropdown>().options.Add(new Dropdown.OptionData { text = "なし" });

            Team1_member = 0;
            Team2_member = 1;
            playTime = 0;
        }
	}

	private void Update(){
        //Debug.Log(Team1_Member);
        //Debug.Log(Team2_Member);
        //Debug.Log(playTime);
    }

    public void OnValueChanged(Dropdown dropdown){
       
        if (SceneManager.GetActiveScene().name == "Config_MainMode_standby_Solo"){
            switch (dropdown.value){

                case 0:
                    playTime = 3;
                    break;
                case 1:
                    playTime = 4;
                    break;
                case 2:
                    playTime = 5;
                    break;
                case 3:
                    playTime = 10;
                    break;
                case 4:
                    playTime = 15;
                    break;
                case 5:
                    playTime = 20;
                    break;
            }

        }

        if (SceneManager.GetActiveScene().name == "Config_PracticeMode_FreePractice_standby"){

            switch (dropdown.value){
                case 0:
                    playTime = 0;
                    break;
                case 1:
                    playTime = 3;
                    break;
                case 2:
                    playTime = 4;
                    break;
                case 3:
                    playTime = 5;
                    break;
                case 4:
                    playTime = 10;
                    break;
                case 5:
                    playTime = 15;
                    break;
                case 6:
                    playTime = 20;
                    break;
            }
        }

    }

    public void OnValueChanged_Team1(Dropdown dropdown){
        switch (dropdown.value){
            case 0:
                Team1_member = 0;
                /*
                for (int i = 0; i < Team1_panel.Length;i++){
                    Team1_panel[i].SetActive(false);
                }*/

                break;
            case 1:
                Team1_member = 1;


                break;
            case 2:
                Team1_member = 2;
                break;
            case 3:
                Team1_member = 3;
                break;
        }

    }

    public void OnValueChanged_Team2(Dropdown dropdown){
        switch (dropdown.value){
            case 0:
                Team2_member = 1;
                break;
            case 1:
                Team2_member = 2;
                break;
            case 2:
                Team2_member = 3;
                break;
            case 3:
                Team2_member = 4;
                break;
        }
      
    }


}