using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller_triangle : MonoBehaviour {

    Image tr;
    float R, G, B;
    public float alfa;
    public float alfaSpeed = 0.05f;

   

    void Start()
    {
        tr = GetComponent<Image>();
        R = GetComponent<Image>().color.r;
        G = GetComponent<Image>().color.g;
        B = GetComponent<Image>().color.b;
        alfa = GetComponent<Image>().color.a;
    }

    void Update()
    {

        StartFarash_triangle();

    }

    void StartFarash_triangle()
    {
        SetAlpha();

        alfa -= alfaSpeed;
        if(alfa <= 0 || alfa >= 1.0){

            alfaSpeed = -alfaSpeed;

        }


    }

    void SetAlpha()
    {
        tr.color = new Color(R, G, B, alfa);
    }

}
