using UnityEngine;
using System.Collections;
using Uniduino;
using System;
using System.IO;

public class AngleSensorScript : MonoBehaviour {

	public Arduino arduino;
	private int pinValueX;
	private int pinValueY;
	private int pinValueZ;
	private double acceleration3done;
	private double acceleration3dtwo;
	/*public double Xone = 0;
	public double Yone = 0;
	public double Zone = 0;
	public double Xtwo = 0;
	public double Ytwo = 0;
	public double Ztwo = 0;
	public double Xdiffer;
	public double Ydiffer;
	public double Zdiffer;
	*/
	private int sele = 0;
	private float differ;
	private double acceleration3d;
	private int kaunto = 0;
	private float acceleration3dkaun = 0;
	private double differ3d = 0;
	private double ofset = 50000;
	private int kauntodiffer = 0;
	private double gurabiteli = 0;

    private bool isKicking_Uniduino;

    public CollisionBall cb;
    public CountDownTimer countdowntimer;



    public float Acceleration3dkaun { get { return acceleration3dkaun; } set { acceleration3dkaun = value; } }
    public bool IsKicking_Uniduino { get { return isKicking_Uniduino; } set { isKicking_Uniduino = value; } }

    /*
    public GameObject cube;

	private bool a = false;
    */

	// Use this for initialization
	void Start () {
		arduino = Uniduino.Arduino.global;
		arduino.Setup (ConfigurePins);
        isKicking_Uniduino = false;

	}
	void ConfigurePins( )
	{
		// Use Analog output 3, 4, 5 pin
		arduino.pinMode(3, PinMode.ANALOG); //x
		arduino.pinMode(4, PinMode.ANALOG); //y
		arduino.pinMode(5, PinMode.ANALOG); //z
        arduino.pinMode(9, PinMode.PWM);

		arduino.reportAnalog(3, 1);
		arduino.reportAnalog(4, 1);
		arduino.reportAnalog(5, 1);
	}

	// Update is called once per frame
	void Update () {
       
        if (cb.IsSnapping == true){
            arduino.analogWrite(9, 150);
            Debug.Log("Snapping");
        }
        else if(cb.IsSnapping == false || countdowntimer.IsFinish == true)
        {
            arduino.analogWrite(9,0);
        }

		pinValueX = pinValueY = pinValueZ = 0;
		int i;
		for (i = 0; i < 100; i++) {
			pinValueX = pinValueX + (arduino.analogRead (3) - 512); 
			pinValueY = pinValueY + (arduino.analogRead (4) - 500);
			pinValueZ = pinValueZ + (arduino.analogRead (5) - 512);
		}


		/*
		Xdiffer = Math.Abs (Xone - Xtwo);
		Ydiffer = Math.Abs (Yone - Ytwo);
		Zdiffer = Math.Abs (Zone - Ztwo);
		
		differ = Math.Pow (Xdiffer, 3) + Math.Pow (Ydiffer, 3) + Math.Pow (Zdiffer, 3);
		*/
		pinValueX = pinValueX / 100;
		pinValueY = pinValueY / 100;
		pinValueZ = pinValueZ / 100;
		acceleration3d = Math.Pow (pinValueX, 2) + Math.Pow (pinValueY, 2) + Math.Pow (pinValueZ, 2);
		acceleration3d = Math.Abs (acceleration3d);
		if(sele == 0) {
			acceleration3done = acceleration3d;
			sele = 1;
		} else {
			acceleration3dtwo = acceleration3d;
			sele = 0;
		}
		gurabiteli = gurabiteli + acceleration3d;

		differ3d = Math.Abs(acceleration3done - acceleration3dtwo) + differ3d;
		kauntodiffer++;
		if(kauntodiffer >= 10){
			if(differ3d < 10000){
				ofset = gurabiteli/10;
								
			}
			kauntodiffer = 0;
			differ3d = 0;
			gurabiteli = 0;
		}
		acceleration3d = acceleration3d - ofset;
		if (acceleration3d > 40000 || kaunto > 0) {
			kaunto++;
            
			acceleration3dkaun = (float)(acceleration3dkaun + acceleration3d);
			if (kaunto > 10) {
				kaunto = 0;
                isKicking_Uniduino = true;
                acceleration3dkaun = 0;
                //File.AppendAllText(@"C:\Myfolder\test.txt",acceleration3dkaun +Environment.NewLine);
            }

        }
        else{
            isKicking_Uniduino = false;
        }
			



		/*double degX = Mathf.Atan2(pinValueX - 507,pinValueZ - 558) / 3.14159 * 180.0 ;     
		double degY = Mathf.Atan2(pinValueY-520,pinValueZ-558) / 3.14159 * 180.0 ;

		cube.transform.rotation = Quaternion.Euler((float)degX, 0, (float)degY);  
		*/
	}
}