using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
 
public class CountDownTimer : MonoBehaviour {
 

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
    public GameObject Camera_ThirdPerson_Dis2_Canvas;
    public GameObject Camera_ThirdPerson_Dis3_Canvas;

    /*
    public SideFences_Ballout SideFences_Ballout;
    public OutofPlayRule OutofPlayRule;
    public GoalPlayRule goalPlayRule;*/

    public GameObject TimeBoad;
    public GameObject TimeBoad_NoTime;

    public GameStartCount gameStartCount;
    //public Replay.ReplayManager Replay;
    private bool isPause;
    private bool isFinish = false;
    public string Pause;
    public string Release;
    private float  speed_p_x_c;
    private float  speed_p_y_c;
    private float speed_p_rotate_c;
    private float speed_p_dash_c;
    private float stamina_p_dash_c;
    private float stamina_p_repair_c;
    private float elapsed_time;

    public GameObject EyeMask;

    private bool isRelease;
       

    private bool one;

    public bool IsPause { get { return this.isPause; } set { this.isPause = value; } }
    public bool IsFinish{ get { return isFinish; } set { isFinish = value; } }
    public float TotalTime{ get { return totalTime; } set { totalTime = value; }}


   
 
    void Start () {
        timerText = GetComponentInChildren<Text>();
        if (SceneManager.GetActiveScene().name == "FreePractice"){
            if (DropdownCallback.PlayTime != 0){
                minute = DropdownCallback.PlayTime;
            }
            if (DropdownCallback.PlayTime == 0){
                timerText.text = "Infinity";
            }
        }
        if(SceneManager.GetActiveScene().name == "mian"){
            minute = DropdownCallback.PlayTime;
        }

       
        seconds = 5;

        totalTime = minute * 60 + seconds;
        oldSeconds = 0f;


        pause_panel.SetActive(false);

        isPause = false;
        isRelease = false;

        isFinish = false;

        Camera_ThirdPerson_Dis2_Canvas.gameObject.SetActive(true);
        Camera_ThirdPerson_Dis3_Canvas.gameObject.SetActive(true);



    }
 
    void Update () {
        pause();
        release();

        speed_p_x_c = gameStartCount.Speed_p_x;
        speed_p_y_c = gameStartCount.Speed_p_y;
        speed_p_rotate_c = gameStartCount.Speed_p_rotate;
        speed_p_dash_c = gameStartCount.Speed_p_dash;

        stamina_p_dash_c = gameStartCount.Stamina_p_dash;
        stamina_p_repair_c = gameStartCount.Stamina_p_repair;


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





            if (totalTime <= 0f)
            {
                Debug.Log("制限時間終了");

                StartCoroutine("wait");

                Camera_ThirdPerson_Dis2_Canvas.gameObject.SetActive(false);
                Camera_ThirdPerson_Dis3_Canvas.gameObject.SetActive(false);


                this.IsFinish = true;
                //Debug.Log(this.isFinish);


            }
        }else{
            timerText.text = "Infinity";
        }
    }


    IEnumerator wait(){
        yield return new WaitForSeconds(1.0f);
    }

   
    public void pause(){
      
        if (Input.GetButtonDown(Pause)|| Input.GetKeyDown(KeyCode.A)){
           
                Debug.Log("push!");


                if (isPause == true)
                {

                    Time.timeScale = 1;

                    isPause = false;
                    pause_panel.SetActive(false);

                }
                else
                {

                    Time.timeScale = 0;
                    pause_panel.SetActive(true);

                    isPause = true;
                    //stamina_p_dash_c = 0;
                    //stamina_p_repair_c = 0;

                }
        }


    }

    public void release(){
        if (SceneManager.GetActiveScene().name == "FreePractice"){
            if (Input.GetButtonDown(Release) || Input.GetKeyDown(KeyCode.A)){

                if (isRelease == true) { 
             
                    isRelease = false;
                    EyeMask.SetActive(false);

                }
                else{

                    EyeMask.SetActive(true);

                    isRelease = true;
                    //stamina_p_dash_c = 0;
                    //stamina_p_repair_c = 0;

                }
            }
        }


    }


}