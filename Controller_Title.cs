using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Controller_Title : MonoBehaviour {

    Text ti;
    float R, G, B;
    public float alfa;
    public float alfaSpeed = 0.01f;
    bool isFrash = true;
	
	void Start () {
        ti = GetComponent<Text>();
        R = GetComponent<Text>().color.r;
        G = GetComponent<Text>().color.g;
        B = GetComponent<Text>().color.b;
        alfa = GetComponent<Text>().color.a;
	}
	
    void Update () {
        
        StartFarash();
       
        }

    void StartFarash(){
        SetAlpha();
        alfa = alfa - alfaSpeed;

         if (alfa <= 0 || alfa >= 1.0){
          alfaSpeed = -alfaSpeed;
          }
    }

    void SetAlpha()
    {
        ti.color = new Color(R, G, B, alfa);
    }


   

	}

