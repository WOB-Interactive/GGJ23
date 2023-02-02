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
    [SerializeField] GameObject preStartScreen;
    [SerializeField] GameObject pausedScreen;
    [SerializeField] GameObject gamePlayScreen;


    [Header("Debug Only")]
    [SerializeField]
    GameStates currentGameState;
    Timer gameTimeLimit;



    private void OnEnable()
    {
        PIckupItem.OnItemPickup += OnItemPickupHandler;
        Timer.OnTimerExpired += OnTimerExpiredHandler;
        GameStateChange += GameStateChangeHandler;
        PlayerFeatures.OnPlayerDeath += OnPlayerDeathHandler;
        DeathZone.OnPlayerKilledInZone += OnPlayerKilledInZoneHandler;
    }

    private void OnDisable()
    {
        PIckupItem.OnItemPickup -= OnItemPickupHandler;
        Timer.OnTimerExpired -= OnTimerExpiredHandler;
        GameStateChange -= GameStateChangeHandler;
        PlayerFeatures.OnPlayerDeath -= OnPlayerDeathHandler;
        DeathZone.OnPlayerKilledInZone -= OnPlayerKilledInZoneHandler;

    }

    // Start is called before the first frame update
    void Start()
    {
        gameTimeLimit = GetComponent<Timer>();
    }

    void ClearPanels() {
        //pausedScreen.SetActive(false);
        //preStartScreen.SetActive(false);
    }

    #region Handlers For gamePlay

    void HandlePauseScreen()
    {
        preStartScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gamePlayScreen.SetActive(false);
        //Time.timeScale = 0; // Quick and dirty but if we wanted to animate the screen, I will need to do something different. Add listeners to Enemies, Hero and pickup Items. 
        // this pattern will allow for ingame cutscenes
        pausedScreen.SetActive(true);
    }
    void HandleIntroScreen()
    {
        gamePlayScreen.SetActive(false);
        preStartScreen.SetActive(true);
    }
    void HandlePlayScreen()
    {
        preStartScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pausedScreen.SetActive(false);
        gamePlayScreen.SetActive(true);


        scoreDisplay.SetText(String.Format("Score: {0}", score));
    }
    void HandleCutSceneScreen() { }
    void HandleEndingScreen() { }
    void HandleGameOverScreen() {
        gamePlayScreen.SetActive(false);
        preStartScreen.SetActive(false);
        pausedScreen.SetActive(false);
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
