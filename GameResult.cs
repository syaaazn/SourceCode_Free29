using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameResult : MonoBehaviour {

    private bool isRunning_Result;
    public Animator[] Result;
    public Text _textScoreResult_A;
    public Text[] _textScoreResult_A_Multi;
    public Text _textScoreResult_E;
    public Text[] _textScoreResult_E_Multi;
    public Text _textJudge;
    public Text[] _textJudge_Multi;
    public GameObject[] gameObject;
    public GameObject[] gameObject_Multi;
    private bool isExit = false;
   

	
	void Start () {
        _textScoreResult_A = _textScoreResult_A.GetComponent<Text>();
        _textScoreResult_E = _textScoreResult_E.GetComponent<Text>();
        _textJudge = _textJudge.GetComponent<Text>();

        _textScoreResult_A.text = "";
        _textScoreResult_E.text = "";
        _textJudge.text = "";
	}
	
	
	void Update () {

        if(Result[0] == true){
            StartCoroutine(ResultAnimation());
        }

        if(isExit == true){

            gameObject[5].SetActive(true);
            gameObject[6].SetActive(true);
           
        }

	}

    IEnumerator ResultAnimation(){
        if (isRunning_Result) { yield break; }

        isRunning_Result = true;

        gameObject[0].gameObject.SetActive(true);
        gameObject_Multi[0].gameObject.SetActive(true);
        gameObject_Multi[1].gameObject.SetActive(true);

        yield return new WaitForSeconds(7.5f);

        _textScoreResult_A.text = ScoreResult.score_A.ToString("0");
        _textScoreResult_E.text = ScoreResult.score_E.ToString("0");
        _textScoreResult_A_Multi[0].text = ScoreResult.score_A.ToString("0");
        _textScoreResult_E_Multi[0].text = ScoreResult.score_E.ToString("0");
        _textScoreResult_A_Multi[1].text = ScoreResult.score_A.ToString("0");
        _textScoreResult_E_Multi[1].text = ScoreResult.score_E.ToString("0");

        gameObject[3].gameObject.SetActive(true);
        gameObject_Multi[6].gameObject.SetActive(true);
        gameObject_Multi[7].gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        gameObject[4].gameObject.SetActive(true);
        gameObject_Multi[8].gameObject.SetActive(true);
        gameObject_Multi[9].gameObject.SetActive(true);



        if(ScoreResult.score_A > ScoreResult.score_E){

            _textJudge.fontSize = 80;
            _textJudge.text = "You win!!";
            _textJudge_Multi[0].fontSize = 80;
            _textJudge_Multi[0].text = "You win!!";
            _textJudge_Multi[1].fontSize = 80;
            _textJudge_Multi[1].text = "You win!!";

        }else if(ScoreResult.score_A < ScoreResult.score_E){

            _textJudge.fontSize = 80;
            _textJudge.text = "You Lose...";
            _textJudge_Multi[0].fontSize = 80;
            _textJudge_Multi[0].text = "You Lose...";
            _textJudge_Multi[1].fontSize = 80;
            _textJudge_Multi[1].text = "You Lose...";

        }else{

            _textJudge.fontSize = 80;
            _textJudge.text = "Draw...!!";
            _textJudge_Multi[0].fontSize = 80;
            _textJudge_Multi[0].text = "Draw...!!";
            _textJudge_Multi[1].fontSize = 80;
            _textJudge_Multi[1].text = "Draw...!!";

        }



        yield return new WaitForSeconds(2.0f);

        gameObject[1].gameObject.SetActive(true);
        gameObject_Multi[4].gameObject.SetActive(true);
        gameObject_Multi[5].gameObject.SetActive(true);
        Result[1].SetBool("isRunnning_JudgePanel", true);
        Result[2].SetBool("isRunnning_JudgePanel", true);
        Result[3].SetBool("isRunnning_JudgePanel", true);



        yield return new WaitForSeconds(2.0f);


        gameObject[2].gameObject.SetActive(true);
        gameObject_Multi[2].gameObject.SetActive(true);
        gameObject_Multi[3].gameObject.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        _textJudge.fontSize = 60;
        _textJudge.text = "リプレイを見ますか?";
        /*
        _textJudge_Multi[0].fontSize = 60;
        _textJudge_Multi[0].text = "リプレイを見ますか?";
        _textJudge_Multi[1].fontSize = 60;
        _textJudge_Multi[1].text = "リプレイを見ますか?";*/
        isExit = true;
        isRunning_Result = false;


    }


   
    public void Continue(){
            FadeManager.Instance.LoadScene("Menu", 1.0f);
    }

    public void Replay()
    {
        //FadeManager.Instance.LoadScene("Replay", 1.0f);
        Application.UnloadLevel("GameOver");
        Resources.UnloadUnusedAssets();

       

    }
}
