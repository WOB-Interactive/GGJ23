using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{

    public static event Action OnTimerExpired;
    public static event Action OnTimerPaused;
    public static event Action OnTimerStarted;
    public static event Action OnTimerRestarted;

    [SerializeField]
    float defaultTimeLimit = 5;
    [SerializeField]
    [Tooltip("Time Limit to set")]
    float timelimit = 5;
    [SerializeField]
    float timeRemaining = 0;
    [SerializeField]
    bool timerRunning = false;
    [SerializeField]
    bool shouldCountDown = true;    
    [SerializeField]
    public TMP_Text TimeLimitTextDisplay;

    public void setTimeLimit(float limit)
    {
        if (shouldCountDown)
        {
            timeRemaining = limit;
        }
        this.timelimit = limit;
    }

    public void stopTimer()
    {
        timerRunning = false;
        OnTimerPaused?.Invoke();

#if UNITY_EDITOR
        Debug.Log(string.Format("{0} Paused {1}", this.name, timeRemaining));
#endif
    }

    public void startTimer()
    {
        timerRunning = true;
        OnTimerStarted?.Invoke();
#if UNITY_EDITOR
        Debug.Log(string.Format("{0} Started {1}", this.name, timeRemaining));
#endif
    }

    public void restart(float limit)
    {

        if (limit != 0)
        {
            timelimit = limit;
            timeRemaining = limit;
        }
        else
        {
            timelimit = defaultTimeLimit;
            timeRemaining = defaultTimeLimit;
        }

        timerRunning = true;
        OnTimerRestarted?.Invoke();

#if UNITY_EDITOR
        Debug.Log(string.Format("{0} Restarted: {1}", this.name, timelimit));
#endif
    }

    public void SetCountDown(float limit)
    {
        timelimit = limit;
        timeRemaining = limit;
    }

    // Update is called once per frame
    void Update()
    {
        if (!timerRunning)
            return;

        if (shouldCountDown)
        {
            this.countDown();
        }
        else
        {
            this.countUp();
        }

        DisplayCounterText();

    }

    void countDown()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            notifyTimeOut();
        }
    }

    void countUp()
    {
        if (timeRemaining < timelimit)
        {
            timeRemaining += Time.deltaTime;
        }
        else
        {
            notifyTimeOut();
        }
    }

    void notifyTimeOut()
    {
        OnTimerExpired?.Invoke();
    }

    public void DisplayCounterText()
    {
        float minutes = Mathf.FloorToInt(timeRemaining / 60);
        float seconds = Mathf.FloorToInt(timeRemaining % 60);
        float milliSeconds = (timeRemaining % 1) * 1000;

        TimeLimitTextDisplay.SetText(string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliSeconds));
    }

    public int GetCounterMinutes()
    {
        return Mathf.FloorToInt(timeRemaining / 60);
    }

    public int GetCounterSeconds()
    {
        return Mathf.FloorToInt(timeRemaining % 60);
    }

}
