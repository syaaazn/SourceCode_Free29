using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GifTextureFolderScript : MonoBehaviour
{

    public GameObject obj;

    public SaveImage saveImage;

    public GameObject startButton;
    public GameObject stopButton;


    public float changeFrameSecond;
    public string folderName;
    public string headText;
    public int imageLength;
    private int firstFrameNum;
    private float dTime;

    private bool isPause = false;
    private bool isMoving = false;

    public int FirstFrameNum{ get { return firstFrameNum; } set { firstFrameNum = value; }}

    // Use this for initialization
    void Start(){
        firstFrameNum = 1;
        dTime = 0.0f;
    }

    // Update is called once per frame
    void Update(){


        //if(isMoving == true){

            dTime += Time.deltaTime;
            if (changeFrameSecond < dTime)
            {
                dTime = 0.0f;
                firstFrameNum++;
            if (firstFrameNum > /*SaveImage.figure_photo*/imageLength) firstFrameNum = 1;
            }
            Texture tex = Resources.Load(folderName + "/" + headText + firstFrameNum) as Texture;
            obj.GetComponent<Renderer>().material.SetTexture("_MainTex", tex);


       // }
           



    }

    /*
    public void MovieStop(){

        Time.timeScale = 0;

    }

    public void MovieStart()
    {

        Time.timeScale = 1;

    }*/

    public void Movie_Start(){
        Time.timeScale = 1;

        startButton.SetActive(false);
        stopButton.SetActive(true);

    
        }

    public void Movie_Stop(){
        Time.timeScale = 0;

        stopButton.SetActive(false);
        startButton.SetActive(true);

    }


    IEnumerator StartReplay(){

        yield return new WaitForSeconds(3.0f);


    }


}