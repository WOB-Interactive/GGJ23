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
    [SerializeField] TMPro.TMP_Text ScoreDisplay;

    [Header("Debug Only")]
    [SerializeField]
    GameStates currentGameState;



    private void OnEnable()
    {
        PIckupItem.OnItemPickup += OnItemPickupHandler;
    }

    private void OnDisable()
    {
        PIckupItem.OnItemPickup -= OnItemPickupHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStateChange += GameStateChangeHandler;
    }

    void GameStateChangeHandler(GameStates new_state)
    {
        currentGameState = new_state;
    }

    void HandlePauseScreen()
    {

    }
    void HandleIntroScreen() { }
    void HandlePlayScreen() {
        ScoreDisplay.SetText(String.Format("Score: {0}", score));
    }
    void HandleCutSceneScreen() { }
    void HandleEndingScreen() { }
    void HandleGameOverScreen() { }

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

    public void OnItemPickupHandler(PickUpItemStruct item)
    {
        score += item.pickupPoints;
    }
}
