using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameEndCount_ThirdPerson : MonoBehaviour
{

    [SerializeField]
    private Text _textCountdown;
    [SerializeField]
    private Image _imageMask;
    public CountDownTimer_ThirdPerson countDownTimer_ThirdPerson;
    public GameStartCount gameStartCount;
    private bool isStart = false;
    private bool isRunning_End = false;
    private float count_r = 1.0f;
    private float count_g = 0.5f;
    private float count_b = 0.5f;

    void Start()
    {
        _textCountdown.text = "";




    }

    private void Update()
    {

        if (countDownTimer_ThirdPerson.TotalTime <= 4.0f)
        {
            _textCountdown.color = new Color(count_r, count_g, count_b);

            StartCoroutine(CountdownCoroutine());

        }

    }

    IEnumerator CountdownCoroutine()
    {
        if (isRunning_End) { yield break; }

        isRunning_End = true;


        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "3";

      
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "2";

      
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "1";

     
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "Finish!";
        gameStartCount.IsStart = false;
      
        yield return new WaitForSeconds(10.0f);

        isRunning_End = false;



    }
}
