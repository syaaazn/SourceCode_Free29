using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GoalPlayRule : MonoBehaviour{
    /// <summary>
    /// 
    /// 
    /// 
    /// 
    /// このスクリプトは選手がフルで用意できていないと動かすことができないので選手が十分に実装できるようになる(NPCの完璧実装)まではコメントアウトしておく(そうしないとNULLリファ吐く)
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    private bool a = false;


    public Text _textOut;
    public Image _imageMask;
    public Text _textOut_third;
    public Image _imageMask_third;
    public GameObject FadeImage;
    public AudioSource GoalSourse;
    public AudioClip[] GoalSound;
    //public Player_move player_Move;
    public GameStartCount gameStartCount;

    public GameObject[] Team1;
    public GameObject[] Team2;
    public GameObject SoccerBall;
    public GameObject Player;

    public BehaiorTree behaiorTree;

    public GameObject Stoppers;

   
    private float alfa;
    private bool isFadeOut = false;
    private bool isFadeIn = false;

    private int resultScore_A;
    private int resultScore_E;

    private int[] Team1_num = new int[4];
    private int[] Team2_num = new int[4];

    private int Team1_Length_start;
    private int Team1_Length_end;

    private int Team2_Length_start;
    private int Team2_Length_end;

    private int index1;
    private int index2;

    List<int> Team1_numbers = new List<int>();
    List<int> Team2_numbers = new List<int>();

    private int Team1_member;
    private int Team2_member;


    //private bool oneFade;

    void Start(){

        //Debug.Log(Team1_Length_end);

        Team1_member = DropdownCallback.Team1_Member;
        Team2_member = DropdownCallback.Team2_Member;

        isFadeOut = false;
        isFadeIn = false;
        //oneFade = true;
        //player_Move = GetComponent<Player_move>();
        //CountSourse = GetComponent<AudioSource>();

        _imageMask.gameObject.SetActive(false);
        _imageMask_third.gameObject.SetActive(false);
        //FadeImage.gameObject.SetActive(false);

        alfa = FadeImage.gameObject.GetComponent<Image>().color.a;
        alfa = 0.0f;
        _textOut.text = "";
        _textOut_third.text = "";

        SetAlpha();

        if(SceneManager.GetActiveScene().name == "FreePractice"){
           
            switch(DropdownCallback.Team1_Member){
                case 0:
                    Team1_num = new int[1];
                    break;
                case 1:
                    Team1_num = new int[2];
                    break;
                case 2:
                    Team1_num = new int[3];
                    break;
                case 3:
                    Team1_num = new int[4];
                    break;
                
            }

            switch (DropdownCallback.Team2_Member){
              
                case 1:
                    Team2_num = new int[1];
                    break;
                case 2:
                    Team2_num = new int[2];
                    break;
                case 3:
                    Team2_num = new int[3];
                    break;
                case 4:
                    Team2_num = new int[4];
                    break;

            }

        }

        Team1_Length_start = 0;
        Team2_Length_start = 0;

        Team1_Length_end = Team1_num.Length - 1;
        Team2_Length_end = Team2_num.Length - 1;

       


        Stoppers.gameObject.SetActive(true);
    }

    void Update(){
        //Debug.Log(CollisionBall.Have_ball_man);

        if(gameStartCount.IsStart == true){
            Stoppers.gameObject.SetActive(false);
        }
       
    }

    void OnTriggerEnter(Collider ball)
    {
        if(this.gameObject.tag == "Goal1"){
            
            if (ball.gameObject.tag == "SoccerBall"){

               // Debug.Log("Goal...");
                ScoreResult.score_E += 1;
                gameStartCount.IsStart = false;
                StartCoroutine("freezetime");

                isFadeOut = true;
                //FadeImage.gameObject.SetActive(true);
                    StartFade();

                StartCoroutine("waitFade");

                    //EndFade();
                //FadeImage.gameObject.SetActive(false);
                 
                //Debug.Log("フェード完了");

                //Player.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            

               
                for (int i = Team1_Length_start; i <= Team1_Length_end; i++){ Team1_numbers.Add(i); }

                while (Team1_numbers.Count > 0){
                    index1 = UnityEngine.Random.Range(0, Team1_numbers.Count);
                    Team1_num[Team1_numbers.Count - 1] = Team1_numbers[index1];

                    Team1_numbers.RemoveAt(index1);
                   
                }

                for (int i = Team2_Length_start; i <= Team2_Length_end; i++){ Team2_numbers.Add(i); }

                while (Team2_numbers.Count > 0){
                    index2 = UnityEngine.Random.Range(0, Team2_numbers.Count);
                    Team2_num[Team2_numbers.Count - 1] = Team2_numbers[index2];

                    Team2_numbers.RemoveAt(index2);
                   
                }

                for (int i = 0; i <= Team1_Length_end; i++){
                        Team1[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }

                   
                for (int i = 0; i <= Team2_Length_end; i++){
                    Team2[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

                SoccerBall.gameObject.GetComponent<Transform>().position = new Vector3(0.0f, 1.0f, 0.0f);
                SoccerBall.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

                for (int i = 0; i <= Team1_Length_end;i++){
                    Team1[Team1_num[i]].gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                          }
                for (int i = 0; i <= Team2_Length_end; i++){
                    Team2[Team2_num[i]].gameObject.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }
                if (DropdownCallback.Team1_Member != 0){

                    if (CollisionBall.Have_ball_man == "Team1"){
                        //Team2[Team2_num[0]].gameObject.GetComponent<CollisionBall>().IsKicking_Start = false;

                        switch(Team2_member){
                            case 1:
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                                break;
                            case 2:
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                                Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(1.5f, 3.5f));
                                break;
                            case 3:
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                                Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(1.5f, 3.5f));
                                Team2[Team2_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team2[Team2_num[2]].gameObject.transform.position.y, Random.Range(5.0f, 8.0f));
                                break;
                            case 4:
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                                Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(1.5f, 3.5f));
                                Team2[Team2_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team2[Team2_num[2]].gameObject.transform.position.y, Random.Range(5.0f, 8.0f));
                                Team2[Team2_num[3]].gameObject.transform.position = new Vector3(Random.Range(2.0f, 5.0f), Team2[Team2_num[3]].gameObject.transform.position.y, Random.Range(5.0f, 8.0f));
                                break;
                        }

                        switch(Team1_member){
                            case 1:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team1[Team1_num[0]].gameObject.transform.position.y, -7.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-11.0f, -10.0f));
                                break;
                            case 2:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team1[Team1_num[0]].gameObject.transform.position.y, -7.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-11.0f, -10.0f));
                                Team1[Team1_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team1[Team1_num[2]].gameObject.transform.position.y, Random.Range(-14.0f, -13.0f));
                                break;
                            case 3:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team1[Team1_num[0]].gameObject.transform.position.y, -7.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-11.0f, -10.0f));
                                Team1[Team1_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team1[Team1_num[2]].gameObject.transform.position.y, Random.Range(-14.0f, -13.0f));
                                Team1[Team1_num[3]].gameObject.transform.position = new Vector3(Random.Range(2.0f, 5.0f), Team1[Team1_num[3]].gameObject.transform.position.y, Random.Range(-14.0f, -13.0f));
                                break;
                        }

                        /*
                        if (gameStartCount.IsStart == false){
                            if (Team2[Team2_num[0]].gameObject.transform.position.z > 0 || Team2[Team2_num[0]].gameObject.transform.position.z < 0){
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                            }
                        }*/
                    }
                }else if(DropdownCallback.Team1_Member == 0){
                    Player.gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Player.gameObject.transform.position.y, -7.0f);
                }

                if (CollisionBall.Have_ball_man == "Team2"){
                    //Team1[Team1_num[0]].gameObject.GetComponent<CollisionBall>().IsKicking_Start = false;

                    if(DropdownCallback.Team1_Member != 0){

                        switch(Team1_member){
                            case 1:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(0.0f, Team1[Team1_num[0]].gameObject.transform.position.y, 0.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-3.5f, -1.5f));
                                break;
                            case 2:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(0.0f, Team1[Team1_num[0]].gameObject.transform.position.y, 0.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-3.5f, -1.5f));
                                Team1[Team1_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team1[Team1_num[2]].gameObject.transform.position.y, Random.Range(-8.0f, -5.0f));
                                break;
                            case 3:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(0.0f, Team1[Team1_num[0]].gameObject.transform.position.y, 0.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-3.5f, -1.5f));
                                Team1[Team1_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team1[Team1_num[2]].gameObject.transform.position.y, Random.Range(-8.0f, -5.0f));
                                Team1[Team1_num[3]].gameObject.transform.position = new Vector3(Random.Range(2.0f, 5.0f), Team1[Team1_num[3]].gameObject.transform.position.y, Random.Range(-8.0f, -5.0f));
                                break;
                        }

                    }
                    if (DropdownCallback.Team1_Member == 0){
                        Player.gameObject.transform.position = new Vector3(0.0f, Player.gameObject.transform.position.y, 0.0f);
                    }
                    
                    switch(Team2_member){
                        case 1:
                            Team2[Team2_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team2[Team2_num[0]].gameObject.transform.position.y, 7.0f);
                            break;
                        case 2:
                            Team2[Team2_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team2[Team2_num[0]].gameObject.transform.position.y, 7.0f);
                            Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(10.0f, 11.0f));
                            break;
                        case 3:
                            Team2[Team2_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team2[Team2_num[0]].gameObject.transform.position.y, 7.0f);
                            Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(10.0f, 11.0f));
                            Team2[Team2_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team2[Team2_num[2]].gameObject.transform.position.y, Random.Range(13.0f, 14.0f));
                            break;
                        case 4:
                            Team2[Team2_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team2[Team2_num[0]].gameObject.transform.position.y, 7.0f);
                            Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(10.0f, 11.0f));
                            Team2[Team2_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team2[Team2_num[2]].gameObject.transform.position.y, Random.Range(13.0f, 14.0f));
                            Team2[Team2_num[3]].gameObject.transform.position = new Vector3(Random.Range(2.0f, 5.0f), Team2[Team2_num[3]].gameObject.transform.position.y, Random.Range(13.0f, 14.0f));
                            break;

                    }

                    /*
                    if (gameStartCount.IsStart == false){
                        if (Team1[Team1_num[0]].gameObject.transform.position.z > 0 || Team1[Team1_num[0]].gameObject.transform.position.z < 0){
                            Team1[Team1_num[0]].gameObject.transform.position = new Vector3(0.0f, Team1[Team1_num[0]].gameObject.transform.position.y, 0.0f);
                        }
                    }*/
                  
                }
            }
        }
       
        if (this.gameObject.tag == "Goal2"){

            if (ball.gameObject.tag == "SoccerBall")
            {

               // Debug.Log("Goal!");
                ScoreResult.score_A += 1;

                gameStartCount.IsStart = false;
                StartCoroutine("freezetime");

                isFadeOut = true;
                //FadeImage.gameObject.SetActive(true);
                    StartFade();
               
                StartCoroutine("waitFade");

                //EndFade();
                //FadeImage.gameObject.SetActive(false);

                //   Debug.Log("フェード完了");

                //Player.gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);


                    for (int i = Team1_Length_start; i <= Team1_Length_end; i++) { Team1_numbers.Add(i); }

                while (Team1_numbers.Count > 0){
                    //Debug.Log(Team1_numbers.Count);
                    index1 = UnityEngine.Random.Range(0, Team1_numbers.Count);
                    //Debug.Log(index1);
                    Team1_num[Team1_numbers.Count - 1] = Team1_numbers[index1];
                    //int ransu = Team1_numbers[index1];
                    //Debug.Log(ransu);
                    //Debug.Log(Team1_num[Team1_numbers.Count-1]);
                    Team1_numbers.RemoveAt(index1);
                    //a = true;
                }

                for (int i = Team2_Length_start; i <= Team2_Length_end; i++) { Team2_numbers.Add(i); }

                while (Team2_numbers.Count > 0){
                    
                    index2 = UnityEngine.Random.Range(0, Team2_numbers.Count);
                    Team2_num[Team2_numbers.Count - 1] = Team2_numbers[index2];

                    Team2_numbers.RemoveAt(index2);

                }

                for (int i = 0; i <= Team1_Length_end; i++){
                    Team1[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }


                for (int i = 0; i <= Team2_Length_end; i++){
                    Team2[i].gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
                SoccerBall.gameObject.GetComponent<Transform>().position = new Vector3(0.0f, 1.0f, 0.0f);
                SoccerBall.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;


                for (int i = 0; i <= Team1_Length_end; i++){
                    Team1[Team1_num[i]].gameObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
                for (int i = 0; i <= Team2_Length_end; i++){
                    Team2[Team2_num[i]].gameObject.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }

                if (DropdownCallback.Team1_Member != 0){
                    if (CollisionBall.Have_ball_man == "Team1"){
                        //Team2[Team2_num[0]].gameObject.GetComponent<CollisionBall>().IsKicking_Start = false;

                        switch (Team2_member)
                        {
                            case 1:
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                                break;
                            case 2:
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                                Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(1.5f, 3.5f));
                                break;
                            case 3:
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                                Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(1.5f, 3.5f));
                                Team2[Team2_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team2[Team2_num[2]].gameObject.transform.position.y, Random.Range(5.0f, 8.0f));
                                break;
                            case 4:
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                                Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(1.5f, 3.5f));
                                Team2[Team2_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team2[Team2_num[2]].gameObject.transform.position.y, Random.Range(5.0f, 8.0f));
                                Team2[Team2_num[3]].gameObject.transform.position = new Vector3(Random.Range(2.0f, 5.0f), Team2[Team2_num[3]].gameObject.transform.position.y, Random.Range(5.0f, 8.0f));
                                break;
                        }


                        switch (Team1_member)
                        {
                            case 1:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team1[Team1_num[0]].gameObject.transform.position.y, -7.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-11.0f, -10.0f));
                                break;
                            case 2:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team1[Team1_num[0]].gameObject.transform.position.y, -7.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-11.0f, -10.0f));
                                Team1[Team1_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team1[Team1_num[2]].gameObject.transform.position.y, Random.Range(-14.0f, -13.0f));
                                break;
                            case 3:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team1[Team1_num[0]].gameObject.transform.position.y, -7.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-11.0f, -10.0f));
                                Team1[Team1_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team1[Team1_num[2]].gameObject.transform.position.y, Random.Range(-14.0f, -13.0f));
                                Team1[Team1_num[3]].gameObject.transform.position = new Vector3(Random.Range(2.0f, 5.0f), Team1[Team1_num[3]].gameObject.transform.position.y, Random.Range(-14.0f, -13.0f));
                                break;
                        }
                        /*
                        if (gameStartCount.IsStart == false){
                            if (Team2[Team2_num[0]].gameObject.transform.position.z > 0 || Team2[Team2_num[0]].gameObject.transform.position.z < 0){
                                Team2[Team2_num[0]].gameObject.transform.position = new Vector3(0.0f, Team2[Team2_num[0]].gameObject.transform.position.y, 0.0f);
                            }
                        }*/
                    }
                }else if(DropdownCallback.Team1_Member == 0){
                    Player.gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Player.gameObject.transform.position.y, -7.0f);
                }
                if (CollisionBall.Have_ball_man == "Team2"){
                    //Team1[Team1_num[0]].gameObject.GetComponent<CollisionBall>().IsKicking_Start = false;
                    if (DropdownCallback.Team1_Member != 0){
                        switch (Team1_member)
                        {
                            case 1:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(0.0f, Team1[Team1_num[0]].gameObject.transform.position.y, 0.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-3.5f, -1.5f));
                                break;
                            case 2:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(0.0f, Team1[Team1_num[0]].gameObject.transform.position.y, 0.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-3.5f, -1.5f));
                                Team1[Team1_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team1[Team1_num[2]].gameObject.transform.position.y, Random.Range(-8.0f, -5.0f));
                                break;
                            case 3:
                                Team1[Team1_num[0]].gameObject.transform.position = new Vector3(0.0f, Team1[Team1_num[0]].gameObject.transform.position.y, 0.0f);
                                Team1[Team1_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team1[Team1_num[1]].gameObject.transform.position.y, Random.Range(-3.5f, -1.5f));
                                Team1[Team1_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team1[Team1_num[2]].gameObject.transform.position.y, Random.Range(-8.0f, -5.0f));
                                Team1[Team1_num[3]].gameObject.transform.position = new Vector3(Random.Range(2.0f, 5.0f), Team1[Team1_num[3]].gameObject.transform.position.y, Random.Range(-8.0f, -5.0f));
                                break;
                        }

                    }
                    if (DropdownCallback.Team1_Member == 0){
                        Player.gameObject.transform.position = new Vector3(0.0f, Player.gameObject.transform.position.y, 0.0f);
                    }

                    switch (Team2_member)
                    {
                        case 1:
                            Team2[Team2_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team2[Team2_num[0]].gameObject.transform.position.y, 7.0f);
                            break;
                        case 2:
                            Team2[Team2_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team2[Team2_num[0]].gameObject.transform.position.y, 7.0f);
                            Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(10.0f, 11.0f));
                            break;
                        case 3:
                            Team2[Team2_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team2[Team2_num[0]].gameObject.transform.position.y, 7.0f);
                            Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(10.0f, 11.0f));
                            Team2[Team2_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team2[Team2_num[2]].gameObject.transform.position.y, Random.Range(13.0f, 14.0f));
                            break;
                        case 4:
                            Team2[Team2_num[0]].gameObject.transform.position = new Vector3(Random.Range(-1.0f, 1.0f), Team2[Team2_num[0]].gameObject.transform.position.y, 7.0f);
                            Team2[Team2_num[1]].gameObject.transform.position = new Vector3(Random.Range(-2.5f, 2.5f), Team2[Team2_num[1]].gameObject.transform.position.y, Random.Range(10.0f, 11.0f));
                            Team2[Team2_num[2]].gameObject.transform.position = new Vector3(Random.Range(-5.0f, -2.0f), Team2[Team2_num[2]].gameObject.transform.position.y, Random.Range(13.0f, 14.0f));
                            Team2[Team2_num[3]].gameObject.transform.position = new Vector3(Random.Range(2.0f, 5.0f), Team2[Team2_num[3]].gameObject.transform.position.y, Random.Range(13.0f, 14.0f));
                            break;

                    }
                    /*
                    if (gameStartCount.IsStart == false){
                        if (Team1[Team1_num[0]].gameObject.transform.position.z > 0 || Team1[Team1_num[0]].gameObject.transform.position.z < 0){
                            Team1[Team1_num[0]].gameObject.transform.position = new Vector3(0.0f, Team1[Team1_num[0]].gameObject.transform.position.y, 0.0f);
                        }
                    }*/
                }
            }
        }
    }

    IEnumerator freezetime(){


        CollisionBall.IsClearance = true;
        Stoppers.gameObject.SetActive(true);
        SetAlpha();
        _imageMask.gameObject.SetActive(true);
        _textOut.gameObject.SetActive(true);
        _imageMask_third.gameObject.SetActive(true);
        _textOut_third.gameObject.SetActive(true);
        GoalSourse.PlayOneShot(GoalSound[0]);

        if(this.gameObject.tag == "Goal1"){
            
            _textOut.text = "Goal...";
            _textOut_third.text = "Goal...";

        }else if(this.gameObject.tag == "Goal2"){
            
            _textOut.text = "Goal!";
            _textOut_third.text = "Goal!";

        }

        yield return new WaitForSeconds(2.0f);

        _imageMask.gameObject.SetActive(false);
        _textOut.gameObject.SetActive(false);

        _imageMask_third.gameObject.SetActive(false);
        _textOut_third.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        //////本番はこいつ消す//////////
        /// 
        CollisionBall.IsClearance = false;
        gameStartCount.IsStart = true;

        Stoppers.gameObject.SetActive(false);
        /// 
        ////////////////////////////


    }

    IEnumerator waitFade(){
        yield return new WaitForSeconds(UnityEngine.Random.Range(3.0f,4.0f));

    }

    IEnumerator waitTime(){
        yield return new WaitForSeconds(UnityEngine.Random.Range(3.0f, 4.0f));
        CollisionBall.have_ball_NPC.Is_shot = true;
    }

    void StartFade(){
        /*
        while(isFadeOut == true){
            //_imageMask_third.gameObject.SetActive(true);
            Debug.Log(alfa);
            alfa += 0.00001f;
            if (alfa >= 1.0f){
                isFadeIn = true;
                isFadeOut = false;
            }

        }*/
    }

        void EndFade(){/*
        while(isFadeIn == true){
            
                alfa -= 0.00001f;
                if (alfa <= 0.0f){
                isFadeIn = false;
                //FadeImage.gameObject.SetActive(false);
                //_imageMask_third.gameObject.SetActive(false);

                }

            }*/
    }

    void SetAlpha()
    {
        alfa = 0.0f;
        FadeImage.gameObject.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, alfa);
    }

}

