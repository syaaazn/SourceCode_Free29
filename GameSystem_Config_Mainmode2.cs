using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class GameSystem_Config_Mainmode2 : MonoBehaviour {

    public GameObject[] gameObject;
    public Text _textConect;
    private bool connected = false;

    void Awake(){
        StartCoroutine(CheckForControllers());
    }


	void Start () {
        _textConect = _textConect.GetComponent<Text>();

        _textConect.text = "DualShock3とOculusを接続、装着してください";
	}
	
    void Update () {

        //ほんとはこのコメントアウトを解除する
        /*
        if (XRDevice.model == ""){
            Debug.Log("XRDevice.model = " + "Nothing!!");
        }
        else{
            Debug.Log("XRDevice.model = " + XRDevice.model);
        }

    */
        /*
        if (connected == true && XRDevice.model != ""){
            _textConect.text = "準備完了!!";

            gameObject[0].SetActive(true);
            gameObject[1].SetActive(true);
            gameObject[2].SetActive(true);

        }
        else if (connected == false && XRDevice.model != ""){
            _textConect.text = "DualShock3を接続してください";

            gameObject[1].SetActive(true);
            gameObject[0].SetActive(false);
        }
        else if (connected == true && XRDevice.model == ""){
            _textConect.text = "Oculusを接続、装着してください";

            gameObject[0].SetActive(true);
            gameObject[1].SetActive(false);

        }else{
            _textConect.text = "DualShock3とOculusを接続、装着してください";

            gameObject[0].SetActive(false);
            gameObject[1].SetActive(false);
            gameObject[2].SetActive(false);
        }
        */
    }



    IEnumerator CheckForControllers(){
        while (true){
            var controllers = Input.GetJoystickNames();

            if (!connected && controllers.Length != 0) {
                
                connected = true;
                Debug.Log("Connected_DualShock3");
               /* gameObject[0].SetActive(true);
                gameObject[1].SetActive(true); */

            } 
            else if(connected && controllers.Length == 0){
                
                connected = false;
                Debug.Log("Disconnected_DualShock3");
                /*gameObject[0].SetActive(false);
                gameObject[1].SetActive(false); */
            }

            yield return new WaitForSeconds(1f);
        }
    }


 
    //gameObject[0].SetActive(true);
    //gameObject[1].SetActive(true);
}
