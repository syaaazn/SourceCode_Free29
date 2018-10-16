using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper_kick : MonoBehaviour {
    private CollisionBall coll;
    private Rigidbody  rigidbody;
    public GameObject soccerball;
    public int myteam;
    public bool Is_shot { get; set; }
	// Use this for initialization
	void Start () {
        coll = GetComponent<CollisionBall>();
        rigidbody = soccerball.GetComponent<Rigidbody>();
        Is_shot = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (coll.w_p != null)
        {
            if (coll.w_p.name == "goalkeeper" + myteam)
            {
                Is_shot = true;
                coll.KickBall();
            }
        }
	}
}
