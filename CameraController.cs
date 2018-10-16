using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CameraController : MonoBehaviour {

    public Camera target;

    void Start(){
        XRDevice.DisableAutoXRCameraTracking(target, true);
        InputTracking.disablePositionalTracking = true;
        XRDevice.DisableAutoXRCameraTracking(target, true);

        Vector3 pos = InputTracking.GetLocalPosition(XRNode.CenterEye);
        this.transform.position -= pos;

    }

    void Update(){
        XRDevice.DisableAutoXRCameraTracking(target, true);

        if(XRDevice.model == ""){
            //Debug.Log("XRDevice.model = " + "Nothing!!");
        }else{
            //Debug.Log("XRDevice.model = " + XRDevice.model);
        }

    }
}
