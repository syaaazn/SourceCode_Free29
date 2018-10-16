using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Coin_move : MonoBehaviour {

    private bool Is_roll;
    private int num;
    public static string color;
    public Text text_;
    public Text text3;
    private bool one;
    public GameObject[] SlidePanel;
    public AnimationClip Cointoss_Up;
    public Animation Cointoss;
    public Animator Coin;
    private bool one_Anime;
    private bool isTurnning;
    public AudioSource CoinSource;
    public AudioClip[] CoinSound;
    private bool limitWait;
    private bool limitCount;
    private float limitsecond;


	void Start () {
        AudioListener.volume = 1.0f;
        limitsecond = 0.0f;
        limitWait = false;
        limitCount = false;
        Time.timeScale = 1.0f;
        isTurnning = false;
        one_Anime = false;
        for (int i = 0; i < 3; i++){
            SlidePanel[i].gameObject.SetActive(true);
        }
        StartCoroutine("StartCoinToss");

        text_.text = "ボタンを押してコイントスをしてください。"+"\n"+"止まった色のチームがキックオフ権を得ます。";
        //text3.text = "×ボタンを押してコイントスをしてください。" + "\n" + "止まった色のチームがキックオフ権を得ます。";
    }
	
	void Update () {
        
        if (Is_roll){
        if (Input.GetButtonDown("Kick") || Input.anyKeyDown){
                one_Anime = true;
                StartCoroutine(Coin_Move());
                Is_roll = false;
                limitCount = true;

            }

        }

        //10秒カウントして入力がなければ勝手に決める
        if(limitCount == true){
            limitsecond += Time.deltaTime;
            if (limitsecond > 10.0f){
                limitWait = true;
            }
        }

        //Debug.Log(limitsecond);
        //Debug.Log(Is_roll);
       	
	}

    IEnumerator StartCoinToss(){
        yield return new WaitForSeconds(7.5f);
        for (int i = 0; i < 3;i++){
            SlidePanel[i].gameObject.SetActive(false);
        }
       
        Is_roll = true;
        one = true;

    }

    IEnumerator Coin_Move(){
        
        if(one_Anime == true){
            Coin.SetBool("isTossing", true);
            CoinSource.PlayOneShot(CoinSound[2]);
            one_Anime = false;
            isTurnning = true;

        }

        /*
        AnimatorStateInfo animInfo = Coin.GetCurrentAnimatorStateInfo(0);
        if (animInfo.normalizedTime < 1.0f)
        {
            Coin.CrossFade("isTurnning",0);
        }*/

        Coin.SetBool("isTurnning", isTurnning);

        //Coin.SetBool("isTossing", false);
        yield return new WaitForSeconds(1.6f);
        //CoinSource.loop = true;
        CoinSource.volume = 0.3f;
        CoinSource.clip = CoinSound[3];
        CoinSource.Play();

        while (one == true){
            
            text_.text = "もう一度ボタンを押して、コイントスを止めてください。";
           // text3.text = "もう一度×ボタンを押して、コイントスを止めてください。";
            transform.rotation = Quaternion.Euler(90, 0, 0);
            yield return null;
            if (Input.GetButtonDown("Kick") || Input.anyKeyDown || limitWait == true){
                yield return new WaitForSeconds(0.2f);
                CoinSource.Stop();
                CoinSource.volume = 1.0f;
               
                if (!Is_roll){
                    num = UnityEngine.Random.Range(0, 2);
                    Coin_Judge(num);
                    //Is_roll = true;
                    one = false;
                    yield break;
                }
            }
            /*
            transform.rotation = Quaternion.Euler(-90, 0, 0);
            yield return null;
            if (Input.GetButtonDown("Kick") || Input.anyKeyDown || limitWait == true){
                yield return new WaitForSeconds(0.2f);
                CoinSource.Stop();
                CoinSource.volume = 1.0f;

                if (!Is_roll){
                    //Debug.Log("safadsfa");
                    num = UnityEngine.Random.Range(0, 2);
                    Coin_Judge(num);
                    Is_roll = true;
                    one = false;
                    yield break;
                }
            }
            */
        }
    }
    void Coin_Judge(int num){
        switch (num){
            case 0:
                Is_roll = false;
                transform.rotation = Quaternion.Euler(90, 0, 0);
                Coin.SetBool("Blue_Flag", true);
                CoinSource.clip = CoinSound[4];
                CoinSource.Play();
                color = "blue";
                text_.text = "あなたのチームがキックオフ権を得ました。";
               // text3.text = "あなたのチームがキックオフ権を得ました。";
                StartCoroutine("waitTime");

                break;
            case 1:
                Is_roll = false;
                transform.rotation = Quaternion.Euler(-90, 0, 0);
                Coin.SetBool("Red_Flag", true);
                CoinSource.clip = CoinSound[4];
                CoinSource.Play();
                color = "red";
                text_.text = "相手チームがキックオフ権を得ました。";
               // text3.text = "相手チームがキックオフ権を得ました。";
                StartCoroutine("waitTime");
               // FadeManager.Instance.LoadScene("mian", 2.0f);

                break;
        }

    }

    IEnumerator waitTime(){
       
        //AudioListener.volume = 0;
        yield return new WaitForSeconds(2.0f);
        FadeManager.Instance.LoadScene("Config_MainMode_2_Solo", 2.0f);
    }
    
}
