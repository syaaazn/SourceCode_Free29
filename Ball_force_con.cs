using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_force_con : MonoBehaviour {
    [HideInInspector]
    public static GameObject joint_man;
    public float changedForce;
    public float changedTorque;
    private float originalForce;
    private float originalTorque;

    private FixedJoint fj;
    private CollisionBall colb;
    private bool Is_enter;
    private Transform ball;

    private int Player;
    private int SnapField;
    private int Team2;
    private int soccerball;
  

    void Start(){
        
         Player = LayerMask.NameToLayer("Player");
        SnapField = LayerMask.NameToLayer("SnapField");
         Team2 = LayerMask.NameToLayer("Team2");
         soccerball = LayerMask.NameToLayer("soccerball");

        ball = this.gameObject.GetComponent<Transform>();

        Is_enter = false;

      
    }


    void Update(){

        //Debug.Log(ball.position);
        if (CollisionBall.IsClearance != true){
            if (ball.position.x > 10.3f){
                //Debug.Log("OverRight!");
                ball.position = new Vector3(10.134f,ball.position.y,ball.position.z);
            }
            else if (ball.position.x < -10.3f){
                //Debug.Log("OverLeft!");
                ball.position = new Vector3(-10.134f,ball.position.y,ball.position.z);
            }
            else if (ball.position.y < 0.665f){
                ball.position = new Vector3(ball.position.x, 0.7f, ball.position.z);
            }

        }


    }

    private void OnCollisionEnter(Collision other){
        //Debug.Log(other.gameObject.name);


       
        if (other.gameObject.tag == "SideFences"){
            
            //
            if (joint_man != null){
                if (!Is_enter)
                {
                    //Debug.Log(joint_man.gameObject.name);

                    Is_enter = true;
                    fj = joint_man.GetComponent<FixedJoint>();
                    /*
                    originalForce = fj.breakForce;
                    originalTorque = fj.breakTorque;
                    fj.breakForce = changedForce;
                    fj.breakTorque = changedTorque;
                    */

                    if (joint_man.name == "Player")
                    {
                        //Debug.Log(joint_man.gameObject.transform.Find("ball_spot").transform.position);

                        //Physics.IgnoreLayerCollision(Player,soccerball);
                        //Physics.IgnoreLayerCollision(SnapField, soccerball);
                        //Physics.IgnoreLayerCollision(Team2, soccerball);

                        this.gameObject.GetComponent<Rigidbody>().mass = 1.0f;

                        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                        colb = joint_man.GetComponent<CollisionBall>();
                        colb.first_force = 0.0f;
                        fj.breakForce = 0;
                        fj.breakTorque = 0;
                        //this.gameObject.GetComponent<Rigidbody>().mass = 0.4f;

                    }

                    if (joint_man.name == "NPC1" || joint_man.name == "NPC2")
                    {
                        //Debug.Log("くっ付く");
                        //Debug.Log(joint_man.gameObject.transform.Find("ball_spot").transform.position);

                        //Physics.IgnoreLayerCollision(Player,soccerball);
                        //Physics.IgnoreLayerCollision(SnapField, soccerball);
                        //Physics.IgnoreLayerCollision(Team2, soccerball);

                        this.gameObject.GetComponent<Rigidbody>().mass = 0.00000000003f;

                        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                        colb = joint_man.GetComponent<CollisionBall>();
                        colb.first_force = 0.0f;
                        //fj.breakForce = 50000000000.0f;
                        //fj.breakTorque = 100000000000.0f;
                        //this.gameObject.GetComponent<Rigidbody>().mass = 0.4f;

                    }
                  
                }
        }
        }else{
            this.gameObject.GetComponent<Rigidbody>().mass = 0.4f;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
           

        }

    }
    private void OnCollisionExit(Collision coll){
        
        if (coll.gameObject.tag == "SideFences"){
            
            if (joint_man != null){
                if (Is_enter)
                {
                    
                   
                    CollisionBall collision = joint_man.GetComponent<CollisionBall>();
                    /*
                    fj.breakForce = originalForce;
                    fj.breakTorque = originalTorque;
                    collision.breakForce = originalForce;
                    collision.breakTorque = originalTorque;
                    */
                    this.gameObject.GetComponent<Rigidbody>().mass = 0.4f;
                   
                    if (joint_man.name == "Player")
                    { 
                        

                        Physics.IgnoreLayerCollision(Player, soccerball,false);
                        Physics.IgnoreLayerCollision(SnapField, soccerball,false);
                       
                        colb = joint_man.GetComponent<CollisionBall>();
                        colb.first_force = 100000f;
                    }

                    if(joint_man.name == "NPC1" || joint_man.name == "NPC2"){
                       // Debug.Log("離れた");
                        colb = joint_man.GetComponent<CollisionBall>();
                        colb.first_force = 100000f;
                        this.gameObject.GetComponent<Rigidbody>().mass = 0.4f;
                    }

                    Is_enter = false;
                   
                }
            }
        }
    }
}
