using UnityEngine;

public class GameInitial //: MonoBehaviour
{

    void start(){
        AudioListener.volume = 1;

    }

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {

        int maxDisplayCount = 3;
        for (int i = 0; i < maxDisplayCount && i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate(1024, 576,60);
        }
        Screen.SetResolution(1024, 576, true, 60);
        Application.targetFrameRate = 60;
    }

    /*void Start()
    {

    }

    //void Update()
    {

    }*/
}