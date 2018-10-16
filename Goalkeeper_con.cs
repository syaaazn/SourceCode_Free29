using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalkeeper_con : MonoBehaviour {
    public float speed;
    public GameObject goalkeeper;
    Rigidbody rd;
    private float PosixZero;
    private bool Is_exit;
	// Use this for initialization
	void Start () {
        rd = goalkeeper.GetComponent<Rigidbody>();
        PosixZero = goalkeeper.transform.position.x;
        Is_exit = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Is_exit)
        {
            if((float)Mathf.Round(goalkeeper.transform.position.x*10)/10 == (float)Mathf.Round(PosixZero*10)/10)
            {
                rd.velocity = Vector3.zero;
                Is_exit = false;
            }
        }
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Soccer Ball")
        {
            //Debug.Log("notnocollider");
                //Debug.Log(goalkeeper.transform.position.x);
                Vector3 hoge = other.transform.position;
                float kyori = hoge.x - goalkeeper.transform.position.x;
                Vector3 idou = (new Vector3(kyori, 0, 0)).normalized;
                rd.velocity = idou*speed;
            //Debug.Log(rd.velocity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Soccer Ball")
        {
//            Debug.Log("jdsk;alfj");
            float distance = PosixZero - goalkeeper.transform.position.x;
            Vector3 idou = (new Vector3(distance, 0, 0)).normalized;
            rd.velocity = idou * speed;
            Is_exit = true;
            //Debug.Log(rd.velocity);
        }

    }

}
