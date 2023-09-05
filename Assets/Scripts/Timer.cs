using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;

    public float time;

    private float seconds;
    private float minutes;


    // Update is called once per frame
    void Start()
    {
        //StartTimer();
    }

    private void Update()
    {
        if (!GameManager.Instance.suspended)
        {
            StartTimer();
            if (time > 0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                time = 0;
                timerText.text = "00:00";
            }
        }
    }

    public void StartTimer()
    {
        minutes = Mathf.FloorToInt(time / 60);
        seconds = Mathf.FloorToInt(time % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
