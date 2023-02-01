using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum GameStates
{
    Intro,
    Play,
    Pause,
    CutScene,
    Ending,
    GameOver,
}


public class GameManager : MonoBehaviour
{   

    int score = 0;
    int threatLevel = 0;
    public static event Action<GameStates> GameStateChange;


    [Header("UI Only")]
    [SerializeField] TMPro.TMP_Text scoreDisplay;
    [SerializeField] GameObject gameOverScreen;

    [Header("Debug Only")]
    [SerializeField]
    GameStates currentGameState;
    Timer gameTimeLimit;



    private void OnEnable()
    {
        PIckupItem.OnItemPickup += OnItemPickupHandler;
        Timer.OnTimerExpired += OnTimerExpiredHandler;
        GameManager.GameStateChange += GameStateChangeHandler;
        PlayerFeatures.OnPlayerDeath += OnPlayerDeathHandler;
        DeathZone.OnPlayerKilledInZone += OnPlayerKilledInZoneHandler;
    }

    private void OnDisable()
    {
        PIckupItem.OnItemPickup -= OnItemPickupHandler;
        Timer.OnTimerExpired -= OnTimerExpiredHandler;
        GameManager.GameStateChange -= GameStateChangeHandler;
        PlayerFeatures.OnPlayerDeath -= OnPlayerDeathHandler;
        DeathZone.OnPlayerKilledInZone -= OnPlayerKilledInZoneHandler;

    }

    // Start is called before the first frame update
    void Start()
    {
        gameTimeLimit = GetComponent<Timer>();
    }

    #region Handlers For gamePlay

    void HandlePauseScreen()
    {

    }
    void HandleIntroScreen() { }
    void HandlePlayScreen() {
        scoreDisplay.SetText(String.Format("Score: {0}", score));
    }
    void HandleCutSceneScreen() { }
    void HandleEndingScreen() { }
    void HandleGameOverScreen() {
        // Force of habit to stop all
        Time.timeScale = 0;
        gameOverScreen.SetActive(true);

    }
    #endregion



    // Update is called once per frame
    void Update()
    {
        switch (currentGameState)
        {
            case GameStates.Intro:
                HandleIntroScreen();
                break;
            case GameStates.Play:
                HandlePlayScreen();
                break;
            case GameStates.Pause:
                HandlePauseScreen();
                break;
            case GameStates.CutScene:
                HandleCutSceneScreen();
                break;
            case GameStates.Ending:
                HandleEndingScreen();
                break;
            case GameStates.GameOver:
                HandleGameOverScreen();
                break;
        }
    }


    #region Custom Event Handlers
    public void OnItemPickupHandler(PickUpItemStruct item)
    {
        score += item.pickupPoints;
    }

    void OnTimerExpiredHandler()
    {

        GameStateChange?.Invoke(GameStates.GameOver);
    }


    void GameStateChangeHandler(GameStates new_state)
    {
        currentGameState = new_state;
    }

    void OnPlayerDeathHandler()
    {
        GameStateChange?.Invoke(GameStates.GameOver);
    }

    void OnPlayerKilledInZoneHandler()
    {
        // here we should put some special graphics to add some flair to the deaths
        GameStateChange?.Invoke(GameStates.GameOver);
    }


    #endregion

}
