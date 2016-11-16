using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

    public GUIText timeText;
    public float totalTime;

    private float timeRemain;
    private string minutesText;
    private string secondsText;
    private float minutes;
    private float seconds;



	// Use this for initialization
	void Start () {
        timeRemain = totalTime;

        UpdateTimeText();

        StartCoroutine(TimeCounting());
	}
	
	// Update is called once per frame
    IEnumerator TimeCounting()
    {
        while (true)
        {
            UpdateTimeRemaining();
            UpdateTimeText();

            yield return new WaitForSeconds(0);
        }
    }

    void UpdateTimeRemaining()
    {
        timeRemain -= Time.deltaTime;
        minutes = Mathf.Floor(timeRemain / 60);
        minutesText = minutes.ToString();
        seconds = Mathf.RoundToInt(timeRemain % 60);
        if(seconds == 60)
        {
            seconds = 59;
        }
        secondsText = seconds.ToString();

        if (minutes < 10)
        {
            minutesText = "0" + minutes.ToString();
        }

        if(seconds < 10)
        {
            secondsText = "0" + seconds.ToString();
        }
    }

    void UpdateTimeText()
    {
        if (timeRemain > 0)
        {
            timeText.text = "Time Remaining: " + minutesText + ":" + secondsText;
        }
        else
        {
            timeText.text = "Time is up!!!";
            StopCoroutine(TimeCounting());
        }
    }
}
