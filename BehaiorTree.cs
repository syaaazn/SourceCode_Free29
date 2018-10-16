using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BehaiorTree : MonoBehaviour
{
    public GameObject airObject;
    public Vector3 shot_point { get; set; }
    public float rotationrate;
    public GameObject ray_point;
    public float Positiveness;
    public int myteam;
    private int enemyteam;
    [HideInInspector]
    public bool Coroutine_flag;
    private bool Isrunning;
    //CollisionBall cb = new CollisionBall();
    private CollisionBall colb;
    public bool Is_shot { get; set; }
    public float Speed;
    private Rigidbody rgd;
    Transform camera;
    private GameObject goalkepper;
    private GameObject goalpoint_enemy;
    private GameObject goalpoint_my;
    private GameObject goal_1;
    private GameObject goal_2;

    private GameObject Goal_enemy;
    public GameObject soccer_ball;
    private GameObject sn_area;
    public GameObject sidefense_1;
    public GameObject sidefense_2;
    private Vector3 now_posi;
    private bool wait_flag;
    public GameStartCount GameStartCount;
    public CollisionBall[] collisions;
    private bool Is_enter;
    public int JudgeNum { get; set; }
    private NPCSystemMain NPCSystem;
    [HideInInspector]
    public string StateYou;
    private bool Late_Coroutine;
    private bool State_Coroutine;
    private bool Is_stop;

    private float stepCount = 0;
    private float stepSpeed;

    public AudioSource NPC_step;
    public AudioClip[] step_sound;

    //public bool Coroutine_Flag{ get { return Coroutine_flag; } set { Coroutine_flag = value; } }

    void Start()
    {
        JudgeNum = 0;
        Late_Coroutine = false;
        State_Coroutine = false;
        Is_stop = false;
        //sn_area = transform.Find("SnapField/ougi/円柱.001").gameObject;
        rotationrate = 20;
        colb = this.gameObject.GetComponent<CollisionBall>();
        //  Debug.Log(colb.w_p);

        //Coroutine_flag = true;
        Isrunning = false;
        Is_shot = false;
        camera = transform.Find("Main Camera");
        rgd = GetComponent<Rigidbody>();
        goal_1 = GameObject.Find("Goal_1");
        goal_2 = GameObject.Find("Goal_2");
        if (myteam == 1)
        {
            enemyteam = 2;
            Goal_enemy = goal_2;

        }
        else if (myteam == 2)
        {
            enemyteam = 1;
            Goal_enemy = goal_1;

        }
        goalkepper = GameObject.Find("goalkeeper" + enemyteam);
        //Debug.Log(goalkepper);
        goalpoint_enemy = GameObject.Find("goal_point" + enemyteam);
        goalpoint_my = GameObject.Find("goal_point" + myteam);
        if(SceneManager.GetActiveScene().name == "mian" ){
            NPCSystem = GameObject.Find("GameSystem_Main").GetComponent<NPCSystemMain>();
        }
        if (SceneManager.GetActiveScene().name == "FreePractice"){
            NPCSystem = GameObject.Find("GameSystem_FreePractice").GetComponent<NPCSystemMain>();
        }


        StateYou = "null";


    }

    /*private void FixedUpdate()
    {
        if (Is_stop)
        {
            rgd.velocity = Vector3.zero;
            Is_stop = false;
        }
    }*/
    void Update()
    {
        //Debug.Log(wait_flag);
        //Debug.Log(cb.who);
        //Debug.Log(CollisionBall.who);
        //Debug.Log(colb.w_p);
        //Debug.Log(rgd.velocity, this.gameObject);
        if (Coroutine_flag)
        {
            NPCSystem.Is_judge_flag1 = true;
            NPCSystem.Is_judge_flag2 = true;

            if (!Probability(Positiveness)&&colb.w_p == null)
            {
                Coroutine_flag = false;
                /*if (Probability(100))
                {*/
                Debug.Log("mamoru",this.gameObject);
                StateYou = "mamoru";
                    float randompoint_x = UnityEngine.Random.Range(sidefense_1.transform.position.x, sidefense_2.transform.position.x);
                float randompoint_z;
                Vector3 go_point = Vector3.zero;
                if(myteam == 1)
                {
                    if (soccer_ball.transform.position.z < goalpoint_my.transform.position.z)
                    {
                        randompoint_z = goalpoint_my.transform.position.z;
                    }
                    else
                    {
                        randompoint_z = UnityEngine.Random.Range(soccer_ball.transform.position.z, goalpoint_my.transform.position.z);
                    }
                     go_point = new Vector3(randompoint_x, transform.position.y, randompoint_z);
                }
                else if(myteam == 2)
                {
                    if(soccer_ball.transform.position.z > goalpoint_my.transform.position.z)
                    {
                        randompoint_z = goalpoint_my.transform.position.z;
                    }
                    else
                    {
                        randompoint_z = UnityEngine.Random.Range(soccer_ball.transform.position.z, goalpoint_my.transform.position.z);
                    }
                     go_point = new Vector3(randompoint_x, transform.position.y, randompoint_z);
                }    
                    
                    GameObject po = Instantiate(airObject, go_point, Quaternion.identity);
                    StartCoroutine(Go_npc(3, po));
                /*}
                else
                {
                    rgd.velocity = Vector3.zero;
                    StartCoroutine(Wait(10));
                }*/

            }
            else
            {
                
                if (JudgeNum == 0)
                {
                    State_Coroutine = true;
                    State_ball();
                }
                else
                {
                    Late_Coroutine = true;
                    //Debug.Log("ToP!!!!!!!!!!!!!");
                    Judge_N();
                }
            }
            ///////////////////足音鳴らすためのコード/////////////////
            /*
            stepCount += stepSpeed;

            if (stepCount >= 1.0f || stepCount <= 0.0f){
                stepSpeed = -stepSpeed;
            }

            if (stepCount >= 1.0f){
                NPC_step.clip = step_sound[0];
                NPC_step.Play();

            }
            */
            //////////////////////////////////////////////////////


        }
        //Debug.Log(Coroutine_flag);
    }
    private void LateUpdate()
    {
        if (Coroutine_flag && Late_Coroutine)
        {
            //Debug.Log("asdfsadf");
            Late_Coroutine = false;
            Coroutine_flag = false;
            
        }
        if (State_Coroutine)
        {
            State_Coroutine = false;
            Coroutine_flag = false;
            NPCSystem.Is_judge_flag1 = true;
        }
        
    }

    private void Judge_N()
    {
        //Coroutine_flag = false;
        //Debug.Log("#######");
        switch (JudgeNum)
        {

            case 1:
                Debug.Log("go-ball", this.gameObject);
                StateYou = "go-ballN";
                JudgeNum = 0;
                StartCoroutine(Go_ball(3, soccer_ball));
                break;
            case 2:
                Debug.Log("NSystem", this.gameObject);
                JudgeNum = 0;
                StateYou = "NSystem";
                float randompoint_x = UnityEngine.Random.Range(sidefense_1.transform.position.x, sidefense_2.transform.position.x);
                float randompoint_z = UnityEngine.Random.Range(goalpoint_my.transform.position.z, goalpoint_enemy.transform.position.z);
                var go_point = new Vector3(randompoint_x, transform.position.y, randompoint_z);
                GameObject po = Instantiate(airObject, go_point, Quaternion.identity);
                StartCoroutine(Go_npc(3, po));
                break;
        }
    }
    private void State_ball()
    {
        // Debug.Log("State_ball");
        //Coroutine_flag = false;
        now_posi = transform.position;
        if (colb.w_p == null)
        {
            string who = Who_ball();
            switch (who)
            {
                case "Team1":
                    if (myteam == 1)
                    {
                        Debug.Log("1-1" + this.gameObject.name,this.gameObject);
                        StateYou = "1-1";
                        float randompoint_x = UnityEngine.Random.Range(sidefense_1.transform.position.x, sidefense_2.transform.position.x);
                        float randompoint_z = UnityEngine.Random.Range(soccer_ball.transform.position.z, goalpoint_enemy.transform.position.z);
                        if(randompoint_z > 20f)
                        {
                            randompoint_z = goalpoint_enemy.transform.position.z;
                        }
                        var go_point = new Vector3(randompoint_x, transform.position.y, randompoint_z);
                        GameObject po = Instantiate(airObject, go_point, Quaternion.identity);
                        StartCoroutine(Go_npc(3, po));
                    }
                    else if (myteam == 2)
                    {
                        if (Probability(50))
                        {
                            Debug.Log("1-2-1" + this.gameObject.name,this.gameObject);
                            StateYou = "go-ball";
                            StartCoroutine(Go_ball(3, soccer_ball));
                        }
                        else
                        {
                            Debug.Log("1-2-2" + this.gameObject.name,this.gameObject);
                            StateYou = "1-2";
                            float randompoint_x = UnityEngine.Random.Range(sidefense_1.transform.position.x, sidefense_2.transform.position.x);
                            float randompoint_z = UnityEngine.Random.Range(soccer_ball.transform.position.z, goalpoint_my.transform.position.z);
                            if(randompoint_z > 20f)
                            {
                                randompoint_z = goalpoint_my.transform.position.z;
                            }
                            var go_point = new Vector3(randompoint_x, transform.position.y, randompoint_z);
                            GameObject po = Instantiate(airObject, go_point, Quaternion.identity);
                            StartCoroutine(Go_npc(3, po));
                        }
                    }
                    break;
                case "Team2":
                    if (myteam == 1)
                    {
                        if (Probability(50))
                        {
                            Debug.Log("2-1-1" + this.gameObject.name,this.gameObject);
                            StateYou = "go-ball";
                            StartCoroutine(Go_ball(3, soccer_ball));
                        }
                        else
                        {
                            Debug.Log("2-1-2" + this.gameObject.name,this.gameObject);
                            StateYou = "2-1";
                            float randompoint_x = UnityEngine.Random.Range(sidefense_1.transform.position.x, sidefense_2.transform.position.x);
                            float randompoint_z = UnityEngine.Random.Range(soccer_ball.transform.position.z, goalpoint_my.transform.position.z);
                            if (randompoint_z < -20)
                            {
                                randompoint_z = goalpoint_my.transform.position.z;
                            }
                            var go_point = new Vector3(randompoint_x, transform.position.y, randompoint_z);
                            GameObject po = Instantiate(airObject, go_point, Quaternion.identity);
                            StartCoroutine(Go_npc(3, po));
                        }
                    }
                    else if (myteam == 2)
                    {
                        Debug.Log("2-2" + this.gameObject.name,this.gameObject);
                        StateYou = "2-2";
                        float randompoint_x = UnityEngine.Random.Range(sidefense_1.transform.position.x, sidefense_2.transform.position.x);
                        float randompoint_z = UnityEngine.Random.Range(soccer_ball.transform.position.z, goalpoint_enemy.transform.position.z);
                        if(randompoint_z < -20)
                        {
                            randompoint_z = goalpoint_enemy.transform.position.z;
                        }
                        var go_point = new Vector3(randompoint_x, transform.position.y, randompoint_z);
                        GameObject po = Instantiate(airObject, go_point, Quaternion.identity);
                        StartCoroutine(Go_npc(3, po));
                    }
                    break;
                case null:
                    Debug.Log("go-ball",this.gameObject);
                    StateYou = "go-ball";
                    StartCoroutine(walk_npc(Speed, soccer_ball));
                    break;
                case "goalkeeper":
                    StateYou = "go-center";
                    float i_randompoint_x = UnityEngine.Random.Range(sidefense_1.transform.position.x, sidefense_2.transform.position.x);
                    float i_randompoint_z = UnityEngine.Random.Range(-10, 10);
                    var i_go_point = new Vector3(i_randompoint_x, transform.position.y, i_randompoint_z);
                    GameObject i_po = Instantiate(airObject, i_go_point, Quaternion.identity);
                    StartCoroutine(Go_npc(3, i_po));
                    break;
                default:
                    //Debug.Log(who);
                    Debug.LogError("該当パターンnot");
                    break;
            }
        }
        else if (colb.w_p != null)
        {
            Debug.Log("go-goal" + this.gameObject.name,this.gameObject);
            StateYou = "go-goal";
            if (!Is_enter)
            {
                StartCoroutine(go_goal(Speed, goalkepper));
            }
            else
            {
                Is_enter = false;
                StartCoroutine(go_goal(Speed, goalkepper));

            }
        }
    }

    public IEnumerator Go_ball(float speed, GameObject spotpoint)
    {
        var newrotation = Quaternion.LookRotation(spotpoint.transform.position - transform.position).eulerAngles;
        newrotation.x = 0;
        newrotation.z = 0;
        rgd.rotation = Quaternion.Euler(newrotation);
        while (true)
        {
            go_went(speed, spotpoint);
            yield return null;
            string who = Who_ball();
            if (colb.w_p != null || who == "Team" + myteam || who == "goalkeeper")
            {
                Coroutine_flag = true;
                yield break;
            }
            if (JudgeNum != 0)
            {
                //Debug.Log("!!!");
                Coroutine_flag = true;
                yield break;
            }
            if (!GameStartCount.IsStart)
            {
                rgd.velocity = Vector3.zero;
                yield break;
            }

        }
    }
    public IEnumerator Go_npc(float speed, GameObject spotpoint)
    {
        //Debug.Log(spotpoint.gameObject.transform.position);
        var newrotation = Quaternion.LookRotation(spotpoint.transform.position - transform.position).eulerAngles;
        newrotation.x = 0;
        newrotation.z = 0;
        rgd.rotation = Quaternion.Euler(newrotation);
        while (true)
        {
            go_went(speed, spotpoint);
            yield return null;
            var me_trans = new Vector3((float)Math.Round(transform.position.x * 1) / 1, 0, (float)Math.Round
            (transform.position.z * 1) / 1);
            var spot_trans = new Vector3((float)Math.Round(spotpoint.transform.position.x * 1) / 1, 0, (float)Math.Round(spotpoint.transform.position.z * 1) / 1);
            if (me_trans == spot_trans || colb.w_p != null)
            {
                Destroy(spotpoint);
                Coroutine_flag = true;
                yield break;
            }
            if (!GameStartCount.IsStart)
            {
                Destroy(spotpoint);
                rgd.velocity = Vector3.zero;
                yield break;
            }


        }
    }
    public IEnumerator walk_npc(float speed, GameObject spotpoint)
    {
        //  Debug.Log("lasjkgg");
        /*var newrotation = Quaternion.LookRotation(spotpoint.transform.position - transform.position).eulerAngles;
        newrotation.x = 0;
        newrotation.z = 0;
        rgd.rotation = Quaternion.Euler(newrotation);*/
        while (true)
        {
            go_went(speed, spotpoint);
            yield return null;
            string who = Who_ball();
            if (who != null || colb.w_p != null)
            {
                rgd.velocity = Vector3.zero;
                Coroutine_flag = true;
                yield break;
            }
            if (JudgeNum != 0)
            {
                //Debug.Log("!!!");
                Coroutine_flag = true;
                yield break;
            }
            if (!GameStartCount.IsStart)
            {
                rgd.velocity = Vector3.zero;
                yield break;
            }


            yield return null;
        }
    }

    public IEnumerator go_goal(float speed, GameObject spotpoint)
    {
        Vector3 targetpos = spotpoint.transform.position;

        targetpos = new Vector3(targetpos.x, transform.position.y, targetpos.z);

        // Debug.Log("ゴール");
        var newrotation = Quaternion.LookRotation(targetpos - transform.position);
        newrotation.x = 0;
        newrotation.z = 0;
        Vector3 Axis_ro = newrotation.eulerAngles;
        Debug.Log(Axis_ro.y);
        if (Axis_ro.y > 180)
        {
            Axis_ro.y = Axis_ro.y - 360f;
        }
        Debug.Log(Axis_ro.y);
        Debug.Log(1f/90f*Axis_ro.y);

        if (colb.w_p == null)
        {
            Coroutine_flag = true;
            yield break;
        }
        StartCoroutine(WaitTime(Mathf.Abs(1f/90f*Axis_ro.y)));
        while (true)
        {
            //Debug.Log("kkkk");
            transform.rotation = Quaternion.Slerp(transform.rotation, newrotation, Time.deltaTime);

            if (wait_flag && colb.w_p != null)
            {
                wait_flag = false;
                break;
            }
            else if (!wait_flag && colb.w_p == null)
            {
                //Debug.Log("asdf");
                Coroutine_flag = true;
                yield break;
            }
            
            yield return null;
        }
        if (Is_enter)
        {
            Is_shot = true;
        }
        while (true)
        {
            go_went(speed, spotpoint);
            if (colb.w_p == null)
            {
                Coroutine_flag = true;
                yield break;
            }
            if (Is_shot)
            {
                GameObject child = Goal_enemy.transform.Find("TransparentGoals").gameObject;
                GameObject child1 = child.transform.Find("TransparentGoal1").gameObject;
                GameObject child2 = child.transform.Find("TransparentGoal2").gameObject;
                GameObject child4 = child.transform.Find("TransparentGoal4").gameObject;
                shot_point = new Vector3(UnityEngine.Random.Range(child1.transform.position.x, child2.transform.position.x), transform.position.y, goalkepper.transform.position.z);

                var _newrotation = Quaternion.LookRotation(spotpoint.transform.position - transform.position).eulerAngles;
                _newrotation.x = 0;
                _newrotation.z = 0;
                rgd.rotation = Quaternion.Euler(_newrotation);
                //Debug.Log(";ajkdkfa");
                //Debug.Log(Is_shot);
                //colb.KickBall();
                //Is_shot = false;
                rgd.velocity = Vector3.zero;
                Coroutine_flag = true;
                yield break;
            }

            if (!GameStartCount.IsStart)
            {
                rgd.velocity = Vector3.zero;
                yield break;
            }


            yield return null;
        }



    }

    IEnumerator WaitTime(float second)
    {
        int i = 0;
        while (i <= second / Time.deltaTime)
        {
            i++;
            if (colb.w_p == null)
            {
                yield break;
            }
            yield return null;
        }
        if (colb.w_p != null)
        {
            wait_flag = true;
        }
    }
    IEnumerator Wait(float second)
    {
        int i = 0;
        Coroutine_flag = false;
        while (i <= second / Time.deltaTime)
        {
            i++;
            rgd.velocity = Vector3.zero;
            yield return null;
        }
        Coroutine_flag = true;

        yield break;
    }

    public bool Probability(float atari)
    {
        float rand = UnityEngine.Random.Range(1f, 100f);
        bool what_bool;
        if (rand <= atari)
        {
            what_bool = true;
        }
        else
        {
            what_bool = false;
        }
        return what_bool;
    }

    void go_went(float speed, GameObject spotpoint)
    {
        int layerMask = ~(LayerMask.GetMask(new string[] { LayerMask.LayerToName(2), LayerMask.LayerToName(11) }));
        Vector3 spot = spotpoint.transform.position - camera.transform.position;
        //Debug.Log(spot);
        var far = transform.forward;
        far = new Vector3(far.x, far.y - 0.7f, far.z);
        Ray ray = new Ray(camera.transform.position, spot);
        Ray ray_for = new Ray(camera.transform.position, far);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
        Debug.DrawRay(ray_for.origin, ray_for.direction * 10, Color.green);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //Debug.LogError(hit.collider.gameObject.transform.root.gameObject.name);
            if (hit.collider.gameObject.transform.root.gameObject.name == spotpoint.gameObject.name)
            {
                //     Debug.Log("OK");
                var newrotation = Quaternion.LookRotation(spotpoint.transform.position - transform.position).eulerAngles;
                newrotation.x = 0;
                newrotation.z = 0;
                rgd.rotation = Quaternion.Euler(newrotation);

                rgd.velocity = transform.forward * speed;
            }
            else
            {
                //Debug.Log("else1");
                rgd.velocity = Vector3.zero;
                float rand = UnityEngine.Random.Range(-rotationrate, rotationrate);

                Quaternion rot = Quaternion.AngleAxis(rand, Vector3.up);
                rgd.rotation = rot * rgd.rotation;
            }


        }
        if (Physics.Raycast(ray_for, out hit, Mathf.Infinity, layerMask))
        {


            //Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));


            if (LayerMask.LayerToName(hit.collider.gameObject.layer) == "MainCoat")
            {
                //         Debug.Log("ok2");
                rgd.velocity = transform.forward * speed;
            }
            else
            {
                //Debug.Log("else2");
                rgd.velocity = Vector3.zero;

                float rand = UnityEngine.Random.Range(-rotationrate, rotationrate);
                /*int rotate = 0;
                double kakudo = 0;
                for (int i = 0; i <= rotationrate * 2; i++)
                {
                    if (i % 2 == 0)
                    {
                        rotate++;
                    }
                    kakudo = (Math.Pow(-1, i)) * rotate;
                    var forwa = new Vector3(ray_point.transform.forward.x, ray_point.transform.forward.y - 0.7f, ray_point.transform.forward.z);
                    var ray_ = Quaternion.Euler(0f, (float)kakudo, 0f) * forwa;
                    Ray ray_ro = new Ray(camera.transform.position, ray_);
                    Debug.DrawRay(ray_ro.origin, ray_ro.direction * 10, Color.blue);
                    RaycastHit hit_ro;
                    if (Physics.Raycast(ray_ro, out hit_ro))
                    {
                        if (hit_ro.collider.gameObject.name == "Main coat")
                        {
                            rand = (float)kakudo;
                        }
                    }
                }*/
                //Quaternion turn = Quaternion.Euler(0f, rand, 0f);
                //rgd.MoveRotation(rgd.rotation * turn);
                Quaternion rot = Quaternion.AngleAxis(rand, Vector3.up);
                rgd.rotation = rot * rgd.rotation;
            }
        }
        if (transform.position.z >= goal_2.transform.position.z || transform.position.z <= goal_1.transform.position.z)
        {
            //Debug.Log("NPCがコート外に出ました。");
            var newrotation = Quaternion.LookRotation(spotpoint.transform.position - transform.position).eulerAngles;
            newrotation.x = 0;
            newrotation.z = 0;
            rgd.rotation = Quaternion.Euler(newrotation);

            rgd.velocity = speed * transform.forward;
        }
    }

    public string Who_ball()
    {
        int nill = 0;
        string[] ball = new string[collisions.Length];
        for (int i = 0; i < collisions.Length; i++)
        {
            ball[i] = collisions[i].who;
            if (ball[i] != null)
            {
                return ball[i];
            }
            else
            {
                nill++;
            }
        }
        if (nill == ball.Length)
        {

            return null;
        }
        else
        {
            return "error";
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        //   
        if (other.gameObject.name == "goal_point" + enemyteam && colb.w_p != null)
        {
            //Debug.Log("コライダー");
            if (!Is_shot)
            {
                //Debug.Log(colb.w_p);
                //Debug.Log("コライダー");
                Is_shot = true;
            }
        }
    }
    /*private void OnCollisionStay(Collision other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.tag == "Team1" || other.gameObject.tag == "Team2")
        {

            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Vector3 Impulse = rigidbody.velocity * -rigidbody.mass;
                rgd.AddForce(Impulse, ForceMode.Impulse);
            }
            //Is_stop = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Team1" || other.gameObject.tag == "Team2")
        {
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                Debug.Log("asdf");
                Vector3 Impulse = rigidbody.velocity * rigidbody.mass;
                rgd.AddForce(Impulse, ForceMode.Impulse);
                rgd.velocity = Vector3.zero;
            }
        }
    }*/
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "goal_point" + enemyteam && colb.w_p != null)
        {
            if (!Is_enter)
            {
                //Debug.Log("in");
                Is_enter = true;
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "goal_point" + enemyteam)
        {
            //Debug.Log("out");
            Is_enter = false;
        }
    }
}