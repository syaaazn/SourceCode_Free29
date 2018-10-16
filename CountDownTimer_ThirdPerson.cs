using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountDownTimer_ThirdPerson : MonoBehaviour
{

    //　トータル制限時間
    private float totalTime;
    //　制限時間（分）
    private int minute;
    //　制限時間（秒）
    private float seconds;
    //　前回Update時の秒数
    private float oldSeconds;
    private Text timerText;
    public GameObject pause_panel;
    private float elapsed_time;

    public GameObject TimeBoad;
    public GameObject TimeBoad_NoTime;

    private bool isPause;
    public string Pause;

    public bool IsPause { get { return this.isPause; } set { this.isPause = value; } }
    public float TotalTime{ get { return totalTime; } set { totalTime = value; }}
 


    void Start(){

        timerText = GetComponentInChildren<Text>();
        if (SceneManager.GetActiveScene().name == "FreePractice")
        {
            if (DropdownCallback.PlayTime != 0)
            {
                minute = DropdownCallback.PlayTime;
            }
            if (DropdownCallback.PlayTime == 0)
            {
                timerText.text = "Infinity";
            }
        }
        if (SceneManager.GetActiveScene().name == "mian")
        {
            minute = DropdownCallback.PlayTime;
        }

        seconds = 5;

        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;
       
        isPause = false;
        pause_panel.SetActive(false);
       
    }

    void Update(){


        if (DropdownCallback.PlayTime != 0){

            //　制限時間が0秒以下なら何もしない
            if (totalTime <= 0f)
            {
                return;
            }
            //　一旦トータルの制限時間を計測；
            totalTime = minute * 60 + seconds;

            if (CollisionBall.IsClearance == true)
            {
                elapsed_time = 0;
            }
            else
                if (CollisionBall.IsClearance == false)
            {
                elapsed_time = Time.deltaTime;
            }
            else
            {
                elapsed_time = Time.deltaTime;
            }


            totalTime -= elapsed_time;
            minute = (int)totalTime / 60;
            seconds = totalTime - minute * 60;

            if ((int)seconds != (int)oldSeconds)
            {
                timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            }
            oldSeconds = seconds;

        }else{
            timerText.text = "Infinity";
        }

        if (Input.GetButtonDown(Pause))
        {
            if (isPause == true)
            {

                Time.timeScale = 1;
                pause_panel.SetActive(false);


                isPause = false;
            }
            else
            {
                
                Time.timeScale = 0;
                pause_panel.SetActive(true);
                isPause = true;
              

            }
        }

        //　制限時間以下になったらGameOver関数でも作ったり、別のシーンに遷移させてゲーム終了処理でも書こうと思う
        if (totalTime <= 0f)
        {

           // Debug.Log("制限時間終了");
           // FadeManager.Instance.LoadScene("GameOver", 1.0f);
        }
    }

}