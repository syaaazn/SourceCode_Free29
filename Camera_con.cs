using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_con : MonoBehaviour {
    public float Camera_speed;
    public string M_vertical;
   

	void Start () {
		
	}
	
	
	void Update () {
        Camera_move();
	}

    private void Camera_move()
    {
        float ry = Input.GetAxis(M_vertical) * Camera_speed;
        transform.Rotate(ry, 0f, 0f);
    }
}
