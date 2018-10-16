using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class GameStartCount : MonoBehaviour
{
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
    public BehaiorTree[] behaiorTree;
    private bool isStart = false;
    private float[] originalSpeed;


    private float speed_p_x;
    private float speed_p_y;
    private float speed_p_rotate;
    private float speed_p_dash;
    private float stamina_p_dash;
    private float stamina_p_repair;

    public GameObject ball;
    private Vector3 ballposi;

    public float Speed_p_x { get { return speed_p_x; } set { speed_p_x = value; }}
    public float Speed_p_y { get { return speed_p_y; } set { speed_p_y = value; } }
    public float Speed_p_rotate { get { return speed_p_rotate; } set { speed_p_rotate = value; } }
    public float Speed_p_dash { get { return speed_p_dash; } set { speed_p_dash = value; }}
    public float Stamina_p_dash{get{ return stamina_p_dash; }set { stamina_p_dash = value; }}
    public float Stamina_p_repair { get { return stamina_p_repair; } set { stamina_p_repair = value; } }
    public bool IsStart{ get { return isStart; }set { isStart = value; }}
    [HideInInspector]
    public bool Is_Coroutine_flag;

    void Start(){
        AudioListener.volume = 1;
        
        isStart = false;
        CountSourse = this.GetComponent<AudioSource>();
        freeze_p = player.gameObject.GetComponent<Player_move>();
        freeze_s = stamina.gameObject.GetComponent<StaminaBar>();
        _textCountdown.text = "";
        _textCountdown_third.text = "";
        StartCoroutine(CountdownCoroutine());
        Is_Coroutine_flag = true;


    }

    private void Update(){
        ballposi = ball.GetComponent<Transform>().position;
        if (ball.gameObject.transform.position.x > 10.3f) { ball.gameObject.transform.position = new Vector3(10.3f, ball.gameObject.transform.position.y, ball.gameObject.transform.position.z); }
        if (ball.gameObject.transform.position.x < -10.3f) { ball.gameObject.transform.position = new Vector3(-10.3f, ball.gameObject.transform.position.y, ball.gameObject.transform.position.z); }
        if (ball.gameObject.transform.position.z > 30.0f) { ball.gameObject.transform.position = new Vector3( ball.gameObject.transform.position.x, ball.gameObject.transform.position.y, 30.0f); }
        if (ball.gameObject.transform.position.z < -30.0f) { ball.gameObject.transform.position = new Vector3(ball.gameObject.transform.position.x, ball.gameObject.transform.position.y, -30.0f); }
        if (ball.gameObject.transform.position.y > 25.0f) { ball.gameObject.transform.position = new Vector3(ball.gameObject.transform.position.x, 25.0f,ball.gameObject.transform.position.z); }
        if (ball.gameObject.transform.position.y < 0.0f) { ball.gameObject.transform.position = new Vector3(ball.gameObject.transform.position.x, 0.0f , ball.gameObject.transform.position.z); }
    }
	
	private void FixedUpdate(){
		
        speed_p_x = freeze_p.M_speed_x;
        speed_p_y = freeze_p.M_speed_y;
        speed_p_rotate = freeze_p.M_rotate_speed_x;
        speed_p_dash = freeze_p.Speed_dash;

        stamina_p_dash = freeze_s.DashStamina;
        stamina_p_repair = freeze_s.RepairStamina;

        if (this.IsStart == false){
            //Player
            speed_p_x = 0;
            speed_p_y = 0;
            speed_p_rotate = 0;
            speed_p_dash = 0;

            stamina_p_dash = 0;
            stamina_p_repair = 0;
            //NPC
            if (!Is_Coroutine_flag){
                
                Is_Coroutine_flag = true;
                for (int i = 0; i < behaiorTree.Length; i++){
                    Rigidbody rigidbody = behaiorTree[i].gameObject.GetComponent<Rigidbody>();
                    rigidbody.velocity = Vector3.zero;
                }
            }



        }

        if (this.IsStart == true){
            //Player
            speed_p_x = 3.5f;
            speed_p_y = 3.5f;
            speed_p_rotate = 100.0f;
            speed_p_dash = 5.5f;

            stamina_p_dash = 0.01f;
            stamina_p_repair = -1;
            //NPC
            if (Is_Coroutine_flag){
                if (behaiorTree != null){
                    Is_Coroutine_flag = false;
                    for (int i = 0; i < behaiorTree.Length; i++){
                        behaiorTree[i].Coroutine_flag = true;
                    }
                }
            }

        }


	}

	IEnumerator CountdownCoroutine(){
      

       
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _imageMask_third.gameObject.SetActive(true);
        _textCountdown_third.gameObject.SetActive(true);
 
        _textCountdown.text = "3";
        _textCountdown_third.text = "3";
        CountSourse.PlayOneShot(CountSound[0]);
        yield return new WaitForSeconds(1.0f);
 
        _textCountdown.text = "2";
        _textCountdown_third.text = "2";
        CountSourse.PlayOneShot(CountSound[0]);
        yield return new WaitForSeconds(1.0f);
 
        _textCountdown.text = "1";
        _textCountdown_third.text = "1";
        CountSourse.PlayOneShot(CountSound[0]);
        yield return new WaitForSeconds(1.0f);
        
        _textCountdown.text = "Start!";
        _textCountdown_third.text = "Start!";
        CountSourse.PlayOneShot(CountSound[1]);
        yield return new WaitForSeconds(1.0f);
 
        _textCountdown.text = "";
        _textCountdown_third.text = "";
        _textCountdown.gameObject.SetActive(false);
        _imageMask.gameObject.SetActive(false);

        _textCountdown_third.gameObject.SetActive(false);
        _imageMask_third.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.025f);

        isStart = true;



       

    }
}