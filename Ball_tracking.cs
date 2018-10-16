using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_tracking : MonoBehaviour {
    public GameObject ball_spot;
    private bool tracking_flag;
    private bool kick_flag;
    public GameObject ball;
    public Rigidbody ball_rigid;
    public float first_force;
	// Use this for initialization
	void Start () {
        ball_rigid = ball.GetComponent<Rigidbody>();
        tracking_flag = false;
        kick_flag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (tracking_flag && !kick_flag)
        {
            ball.transform.position = ball_spot.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Kick"))
        {
            kick_flag = true;
            if (tracking_flag && kick_flag)
            {
                Vector3 kick_force = ball_spot.transform.forward * first_force;
                ball_rigid.velocity = kick_force;
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SoccerBall")
        {
            tracking_flag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("asf");
        if(other.gameObject.tag == "SoccerBall")
        {
            tracking_flag = false;
            kick_flag = false;
        }
    }
}
