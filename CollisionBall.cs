using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollisionBall : MonoBehaviour
{

    [HideInInspector]
    public string who;
    enum Mode
    {
        collision,
        trigger
    };

    private Mode mode;

    [HideInInspector]
    public FixedJoint fixedJoint;
    //　外れる力
    [HideInInspector]
    public float breakForce;
    //　外れる角度
    [HideInInspector]
    public float breakTorque;
    // キックする力
    [HideInInspector]
    public float first_force;

    [SerializeField]
    public GameObject ball_spot;
    public GameObject ball;

    //　スナップしているかどうか
    private bool isSnapping;

    public AudioSource kick_a;
    public AudioSource kick_b;
    public AudioClip[] kick_sound;

    public AudioSource KeeperSource;
    public AudioClip[] KeeperSound;
    [HideInInspector]
    public Transform w_p;
    private Rigidbody ball_rigid;

    public Text _textOut;
    public Image _imageMask;
    public Text _textOut_third;
    public Image _imageMask_third;
    public Text SituationText;


    public float theta;
    public string Kick;
    public GameObject sidefence1;
    public GameObject sidefence2;
    private BehaiorTree bt;
    private Goalkeeper_kick gok;
    private bool Is_wait = true;

    private static string have_ball_man;
    private static bool isClearance = false;
    private bool isKicking = false;
    private string have_ball_now;
    public static BehaiorTree have_ball_NPC;
    private bool isKicking_Start;

    public NPCInstance nPCInstance;

    public AngleSensorScript Arduino;

    private bool isRunning = false;

    public static bool IsClearance { get { return isClearance; } set { isClearance = value; } }
    public static string Have_ball_man { get { return have_ball_man; } set { have_ball_man = value; } }
    public bool IsKicking { get { return isKicking; } set { isKicking = value; } }
    public bool IsKicking_Start{ get { return isKicking_Start; } set { isKicking_Start = value; } }
    public bool IsSnapping { get { return isSnapping; } set { isSnapping = value; } }
   
    
    void Start(){
        IsKicking_Start = true;
        CollisionBall.IsClearance = false;
        IsKicking = false;

        switch (this.gameObject.name){
            case "NPC1":
            case "NPC2":
                breakForce = 500;
                breakTorque = 100000000f;
                first_force = 170000;
                break;
            case "goalkeeper1":
            case "goalkeeper2":
                breakForce = 30000;
                breakTorque = 100000f;
                break;
            case "Player":
                breakForce = 45000f;
                breakTorque = 20000f;
                first_force = 100000f;
                break;
           
        }
        
        bt = GetComponent<BehaiorTree>();
        gok = GetComponent<Goalkeeper_kick>();
        kick_a = GetComponent<AudioSource>();
        kick_b = ball.GetComponent<AudioSource>();
        ball_rigid = ball.GetComponent<Rigidbody>();
    

        if (mode == Mode.collision)
        {
            GetComponentInChildren<Collider>().isTrigger = false;
        }
        else if (mode == Mode.trigger)
        {
            GetComponentInChildren<Collider>().isTrigger = true;
        }
        who = null;
        w_p = null;
        have_ball_now = null;
        //SituationText.gameObject.SetActive(false);
    }

	void LateUpdate(){

        //Debug.Log(this.gameObject.name + ball_spot.gameObject.transform.forward);
        //Debug.Log(this.gameObject.name + (ball_spot.gameObject.transform.forward * first_force));

        KickBall();
        //Debug.Log("IsKicking_Start " + IsKicking_Start);
        //Debug.Log(isSnapping);
        //Debug.Log(bt.Is_shot);
        //Debug.Log(Input.GetButtonDown(Kick));
       // Debug.Log(fixedJoint,this.gameObject);
        //Debug.Log(who);
        //Debug.Log(w_p);
        //Debug.Log(CollisionBall.IsClearance);

        //Debug.Log(have_ball_now,this.gameObject);



    }
    
    void OnCollisionEnter(Collision col){
        if (!isSnapping){
            JudgePlayer(col.collider);
        }
    }


    void OnTriggerEnter(Collider col){
        if (!isSnapping){
            JudgePlayer(col);
        }

        if (this.gameObject.name == "NPC2"){
           // if (col.gameObject.name == "Player"){
            //Debug.Log(col);
                nPCInstance.LoadAudio();

           // }

        }
    }

    void JudgePlayer(Collider col){

        if(this.gameObject.name == "NPC1" || this.gameObject.name == "NPC2"){
            //Debug.Log("SnapNPC");
            have_ball_NPC = this.gameObject.GetComponent<BehaiorTree>();
        }

        if (col.gameObject.tag == "SoccerBall"){
            
            if (fixedJoint == null){
                if (this.gameObject.name == "NPC1" || this.gameObject.name == "NPC2")
                {
                    if (bt.Who_ball() == null)
                    {

                        have_ball_man = this.gameObject.tag;
                        have_ball_now = this.gameObject.name;
                    }
                }
                if (this.gameObject.name == "Player" && w_p == null)
                {
                    have_ball_man = this.gameObject.tag;
                    have_ball_now = this.gameObject.name;
                }

                if(this.gameObject.tag == "goalkeeper"){

                    if(this.gameObject.transform.position.z < 0){
                        have_ball_man = "Team1";
                    }
                    if (this.gameObject.transform.position.z > 0)
                    {
                        have_ball_man = "Team2";
                    }

                    CollisionBall.IsClearance = true;
                    KeeperSource.PlayOneShot(KeeperSound[0]);

                    StartCoroutine("freezetime");

                }/*else{
                    this.IsClearance = false;
                }*/
                Debug.Log(have_ball_man, this.gameObject);

                this.gameObject.AddComponent<FixedJoint>();
                fixedJoint = this.gameObject.GetComponent<FixedJoint>();
                ball.transform.position = ball_spot.transform.position;
                ball.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                fixedJoint.connectedBody = col.gameObject.GetComponent<Rigidbody>();

                if (have_ball_now == "Player"){
                    SituationText.gameObject.SetActive(true);
                    SituationText.text = "ボールスナッピング中!";
                    first_force = 100000f;
                    kick_b.clip = kick_sound[1];
                    kick_b.Play();

                }

                fixedJoint.breakForce = breakForce;
                fixedJoint.breakTorque = breakTorque;
                fixedJoint.enableCollision = true;
                isSnapping = true;
                Ball_force_con.joint_man = this.gameObject;
                
                w_p = this.GetComponent<Transform>().root;
                
                who = w_p.tag;
                GetComponent<Rigidbody>().Sleep();
                //Debug.Log(w_p.tag);
                //Debug.Log(who);

            }
        }


    }

    public void KickBall(){

        switch (this.gameObject.name){
            case "Player":
            case "SnapField":

            if (isSnapping == true){
                    
                    
                //Debug.Log(bt.Is_shot);
                if (Input.GetButtonDown(Kick)){
                    //isSnapping = false;
                   
                    kick_a.PlayOneShot(kick_sound[0]);
                        //fixedJoint.enableCollision = false;
                        IsKicking = true;
                        IsKicking_Start = true;
                        
                        first_force = 100000f;
                        Debug.Log(first_force);
                        Vector3 kick_force = ball_spot.gameObject.transform.forward * first_force;
                        kick_force.y = 0;
                        ball_rigid.AddForce(kick_force);

                        IsKicking = false;
                }

                if (Arduino.IsKicking_Uniduino == true){

                        kick_a.PlayOneShot(kick_sound[0]);
                        //fixedJoint.enableCollision = false;
                        IsKicking = true;
                        IsKicking_Start = true;

                        first_force = Arduino.Acceleration3dkaun * 0.6f;
                        Debug.Log(first_force,this.gameObject);
                        Vector3 kick_force = ball_spot.gameObject.transform.forward * first_force;
                        kick_force.y = 0;
                        ball_rigid.AddForce(kick_force);

                        IsKicking = false;
                    }
            }
                break;
            case "NPC1":
            case "NPC2":

                if (bt.Is_shot)
                {
                   // Debug.Log("Kick!");
                    //isSnapping = false;
                    //fixedJoint.breakForce = 50;
                    //fixedJoint.breakTorque = 400;
                    kick_a.PlayOneShot(kick_sound[0]);

                    IsKicking = true;
                    IsKicking_Start = true;
                    Vector3 shot_ = (bt.shot_point - ball_spot.transform.position);
                    shot_.y = 0;
                    shot_ = shot_.normalized;
                    Vector3 kick_force = shot_*first_force;
                    //kick_force.y = 0;
                    ball_rigid.AddForce(kick_force);
                    IsKicking = false;
                    bt.Is_shot = false;
                    bt.Coroutine_flag = true;
                }
                break;
            case "goalkeeper1":
                if (w_p != null){
                    if (w_p.gameObject.name == gameObject.name){
                        if (Is_wait){
                          //  Debug.Log("ghjkl");
                            Is_wait = false;
                            StartCoroutine(WaitTime(3, -10,0));
                            break;
                        }
                        break;
                    }
                    else { break; }
                }
                else { break; }
                
            case "goalkeeper2":
                if (w_p != null){
                    if (w_p.gameObject.name == gameObject.name){

                        if (Is_wait){
                            //Debug.Log("ghjkl");
                            Is_wait = false;
                            StartCoroutine(WaitTime(3, 0, 10));
                            break;
                        }
                        break;
                    }
                    else
                    {

                        break;
                    }

                }
                else { break; }
        }


    }
    void ShotBall_keeper(float x1,float x2){
        
        Vector3 ball_point = new Vector3(UnityEngine.Random.Range(sidefence1.transform.position.x, sidefence2.transform.position.x), transform.position.y, UnityEngine.Random.Range(x1,x2));
        //Debug.Log(ball_point);
        Vector2 startPos = new Vector2(ball_spot.transform.position.x, ball_spot.transform.position.z);
        Vector2 targetPos = new Vector2(ball_point.x, ball_point.z);
        float distance = Vector2.Distance(targetPos, startPos);
        float x = distance;
        float g = Physics.gravity.y;
        float y0 = ball_spot.transform.position.y;
        float y = ball_point.y;

        float rad = theta * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float tan = Mathf.Tan(rad);
        float v = Mathf.Sqrt(g * x * x / (2 * cos * cos * (y - y0 - x * tan)));

        Vector3 startPos_3 = ball_spot.transform.position;
        Vector3 targetPos_3 = ball_point;
        startPos_3.y = 0;
        targetPos_3.y = 0;
        Vector3 dir = (targetPos_3 - startPos_3).normalized;
        Quaternion yawRot = Quaternion.FromToRotation(Vector3.right, dir);
        Vector3 vec = v * Vector3.right;

        vec = yawRot * Quaternion.AngleAxis(theta, Vector3.forward) * vec;
        Vector3 force = vec * ball_rigid.mass;
        Destroy(fixedJoint);
        isSnapping = false;
        w_p = null;
        //Debug.Log(w_p);
        who = null;
        ball_rigid.velocity = Vector3.zero;
        ball_rigid.AddForce(force, ForceMode.Impulse);

        CollisionBall.IsClearance = false;
        KeeperSource.PlayOneShot(KeeperSound[0]);
        Ball_force_con.joint_man = null;
        Is_wait = true;
    }
    IEnumerator WaitTime(float time,float x1,float x2){
        yield return new WaitForSeconds(UnityEngine.Random.Range(1, time));
        ShotBall_keeper(x1, x2);
    }


    IEnumerator freezetime(){
        _imageMask.gameObject.SetActive(true);
        _textOut.gameObject.SetActive(true);
        _imageMask_third.gameObject.SetActive(true);
        _textOut_third.gameObject.SetActive(true);



        _textOut.text = "CatchBall!";
        _textOut_third.text = "CatchBall!";



        yield return new WaitForSeconds(1.0f);

        _imageMask.gameObject.SetActive(false);
        _textOut.gameObject.SetActive(false);

        _imageMask_third.gameObject.SetActive(false);
        _textOut_third.gameObject.SetActive(false);

    }

	//　ジョイントが解除された時に呼ばれる
	void OnJointBreak(){

        if(have_ball_now == "Player"){
            SituationText.gameObject.SetActive(false);
           // ball.GetComponent<AudioSource>().volume = VolumeValue.DefaultValue_SnappingSound;
            kick_b.clip = kick_sound[2];
        }


       // Debug.Log("Snap外れた");
        //Debug.Log("解除");
        fixedJoint = null;
        isSnapping = false;
        w_p = null;
        //Debug.Log(w_p);
        who = null;
        //Debug.Log(who);
        Ball_force_con.joint_man = null;

        kick_b.Play();
    }
}