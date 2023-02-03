using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Storage : MonoBehaviour
{



    public static event Action<int> OnHighScore; 
    private const string highscorePrefs = "highscore";
    private const string fxLevel = "fx";
    private const string bgLevel = "bg";
    private const string audioEndabled = "audio_on";
    private const string closedCaptionEnabled = "caption_on";

    private const string daysSearchings = "Days Searching";
    private const string deathTrials = "deaths"; // todo simple track for how many deaths


    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(Storage.highscorePrefs);
    }

    public static void SetHighscore(int score)
    {

        if (score > Storage.GetHighScore())
        {
            PlayerPrefs.SetInt(Storage.highscorePrefs, score);
            OnHighScore?.Invoke(score);
        }
    }

    public static void SetAudioEnabled(bool enabled)
    {
        PlayerPrefs.SetInt(Storage.audioEndabled, enabled ? 1 : 0);
    }
    public static bool GetAudioEnabled()
    {
        return PlayerPrefs.GetInt(Storage.audioEndabled, 1) == 1;
    }


    public static void SetClosedCaptionEnabled(bool enabled)
    {
        PlayerPrefs.SetInt(Storage.closedCaptionEnabled, enabled ? 1 : 0);
    }
    public static bool GetClosedCaptionEnabled()
    {
        return PlayerPrefs.GetInt(Storage.closedCaptionEnabled, 1) == 1;
    }

    public static void SetAudioBGLevel(float level)
    {
        PlayerPrefs.SetFloat(Storage.bgLevel, level);
    }

    public static void SetAudioFXLevel(float level)
    {
        PlayerPrefs.SetFloat(Storage.fxLevel, level);
    }

    public static float GetBGLevel()
    {
        return PlayerPrefs.GetFloat(Storage.bgLevel, .75f);
    }

    public static float GetFXLevel()
    {
        return PlayerPrefs.GetFloat(Storage.fxLevel, 0.5f);
    }

    public static void ResetHighScore()
    {
        PlayerPrefs.SetInt(Storage.highscorePrefs, 10000);
    }

    public static void ResetDaysSearch()
    {
        PlayerPrefs.SetInt(Storage.daysSearchings, 0);
    }

    public static int GetDaysSearching()
    {
        return PlayerPrefs.GetInt(Storage.daysSearchings, 0);
    }

    public static void AddDaysSearching(int cnt)
    {
        PlayerPrefs.SetInt(Storage.daysSearchings, GetDaysSearching() + cnt);
    }


}