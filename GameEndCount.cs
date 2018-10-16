using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEndCount : MonoBehaviour {
    
    [SerializeField]
    private Text _textCountdown;
    [SerializeField]
    private Text _textCountdown_third;
    [SerializeField]
    private Image _imageMask;
    [SerializeField]
    private Image _imageMask_third;
    private Player_move freeze_p;
    private StaminaBar freeze_s;
    public GameObject player;
    private AudioSource CountSourse;
    public AudioClip[] CountSound;
    public GameObject stamina;
    public GameObject[] gameObjects;
    public CountDownTimer CountDownTimer;
    public GameStartCount gameStartCount;
    private bool isStart = false;
    private bool isRunning_End = false;
    private bool isCoroutine = true;
    private float count_r = 1.0f;
    private float count_g = 0.5f;
    private float count_b = 0.5f;


    private float speed_p_x;
    private float speed_p_y;
    private float speed_p_rotate;
    private float speed_p_dash;
    private float stamina_p_dash;
    private float stamina_p_repair;

    public float Speed_p_x { get { return speed_p_x; } set { speed_p_x = value; } }
    public float Speed_p_y { get { return speed_p_y; } set { speed_p_y = value; } }
    public float Speed_p_rotate { get { return speed_p_rotate; } set { speed_p_rotate = value; } }
    public float Speed_p_dash { get { return speed_p_dash; } set { speed_p_dash = value; } }
    public float Stamina_p_dash { get { return stamina_p_dash; } set { stamina_p_dash = value; } }
    public float Stamina_p_repair { get { return stamina_p_repair; } set { stamina_p_repair = value; } }
    public bool IsStart { get { return isStart; } set { isStart = value; } }

    void Start()
    {
        CountSourse = this.GetComponent<AudioSource>();
        freeze_p = player.gameObject.GetComponent<Player_move>();
        freeze_s = stamina.gameObject.GetComponent<StaminaBar>();
        _textCountdown.text = "";
        _textCountdown_third.text = "";



       
    }

    private void Update()
    {

        speed_p_x = freeze_p.M_speed_x;
        speed_p_y = freeze_p.M_speed_y;
        speed_p_rotate = freeze_p.M_rotate_speed_x;
        speed_p_dash = freeze_p.Speed_dash;

        stamina_p_dash = freeze_s.DashStamina;
        stamina_p_repair = freeze_s.RepairStamina;

       

        if (gameStartCount.IsStart == false)
        {
            speed_p_x = 0;
            speed_p_y = 0;
            speed_p_rotate = 0;
            speed_p_dash = 0;
        }



        freeze_s.DashStamina = 0;
        freeze_s.RepairStamina = 0;


        if (gameStartCount.IsStart == true)
        {
            speed_p_x = 3.5f;
            speed_p_y = 3.5f;
            speed_p_rotate = 100.0f;
            speed_p_dash = 5.5f;

            freeze_s.DashStamina = 0.01f;
            freeze_s.RepairStamina = 0;

        }

        if(isCoroutine == true){
            if (CountDownTimer.TotalTime <= 4.0f)
            {
                _textCountdown.color = new Color(count_r, count_g, count_b);
                _textCountdown_third.color = new Color(count_r, count_g, count_b);

                StartCoroutine(CountdownCoroutine());


            }
        }


    }

    IEnumerator CountdownCoroutine()
    {
        
        if (isRunning_End){ yield break; }
           
        isRunning_End = true;
        isCoroutine = true;


        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _imageMask_third.gameObject.SetActive(true);
        _textCountdown_third.gameObject.SetActive(true);


        _textCountdown.text = "3";
        _textCountdown_third.text = "3";
       
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _imageMask_third.gameObject.SetActive(true);
        _textCountdown_third.gameObject.SetActive(true);


        CountSourse.PlayOneShot(CountSound[0]);
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "2";
        _textCountdown_third.text = "2";

        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _imageMask_third.gameObject.SetActive(true);
        _textCountdown_third.gameObject.SetActive(true);

       
        CountSourse.PlayOneShot(CountSound[0]);
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "1";
        _textCountdown_third.text = "1";

        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _imageMask_third.gameObject.SetActive(true);
        _textCountdown_third.gameObject.SetActive(true);


        CountSourse.PlayOneShot(CountSound[0]);
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "Finish!";
        _textCountdown_third.text = "Finish!";

        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _imageMask_third.gameObject.SetActive(true);
        _textCountdown_third.gameObject.SetActive(true);


        gameStartCount.IsStart = false;
        CountSourse.PlayOneShot(CountSound[1]);
        yield return new WaitForSeconds(5.0f);

        isRunning_End = false;
        isCoroutine = false;

        _imageMask.gameObject.SetActive(false);
        _textCountdown.gameObject.SetActive(false);

        _imageMask_third.gameObject.SetActive(false);
        _textCountdown_third.gameObject.SetActive(false);

        gameObjects[0].SetActive(false);
        gameObjects[1].SetActive(false);

        count_r = 255.0f;
        count_g = 255.0f;
        count_b = 255.0f;


        _textCountdown.color = new Color(count_r, count_g, count_b);
        _textCountdown_third.color = new Color(count_r, count_g, count_b);


        /*
        gameObjects[2].SetActive(true);

        gameObjects[3].SetActive(true);
        gameObjects[4].SetActive(true);

        gameObjects[5].SetActive(true);
        */
    }
}
