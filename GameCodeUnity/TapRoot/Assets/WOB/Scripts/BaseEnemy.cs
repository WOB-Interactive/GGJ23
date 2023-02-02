using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;



public enum EnemyStates
{
    Idle,
    Following,    
    Notify,
    Attack,
}

public class BaseEnemy : MonoBehaviour
{

    NavMeshAgent meshAgent;
    [SerializeField]
    GameObject playerLoc;
    [SerializeField]
    [Range(0.2f, 10f)]
    float distanceToFollow = 0.5f;
    [SerializeField]
    EnemyStates currentState;
    [SerializeField]
    [Range(0.2f, 10f)]
    float speed = 0.5f;

    [Header("SOUND FX")]
    [SerializeField]
    string enemyNearSoundFX;

    [SerializeField]
    Collider scentArea;

    bool canMove = false;

    [SerializeField]
    [Range(1.0f, 10f)]
    float mindWipeRate = 5;


    private void OnEnable()
    {
        EnemyNear.ItemToFollow += OnItemToFollowHandler;
        Companion.CompanionDead += OnCompanionDeadHandler;
        GameManager.GameStateChange += OnGameStateChangeHandler;
    }

    private void OnDisable()
    {
        EnemyNear.ItemToFollow -= OnItemToFollowHandler;
        Companion.CompanionDead -= OnCompanionDeadHandler;
        GameManager.GameStateChange -= OnGameStateChangeHandler;
    }

    void OnItemToFollowHandler(Transform transform)
    {
        playerLoc = transform.gameObject;
        currentState = EnemyStates.Following;
        //todo trigger YELL/HISSSSSS
    }

    void OnCompanionDeadHandler()
    {
        HandleIdle();
    }

    void OnGameStateChangeHandler(GameStates state)
    {
        switch(state)
        {
            case GameStates.Play:
                // MOVEABLE
                canMove = true;
                break;
            default:
                //not moving
                canMove = false; 
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        meshAgent.speed = speed;
        meshAgent.stoppingDistance = distanceToFollow;

        InvokeRepeating("HandleIdle", 1.0f, mindWipeRate);

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            switch (currentState)
            {
                case EnemyStates.Following:
                    HandleFollowPlayer();
                    break;
                case EnemyStates.Idle:
                    HandleIdle();
                    break;
            }
        }
    }

    [ContextMenu("Clear Hunt")]
    void HandleIdle()
    {
        currentState = EnemyStates.Idle;
        scentArea.enabled = true;
        playerLoc = null;
        // todo animation 
        //animation?.SetBool("FollowPlayer", false );
        
    }

    void HandleFollowPlayer()
    {
        if (playerLoc != null)
        {
            scentArea.enabled = false;
            // update for traking player
            meshAgent.SetDestination(playerLoc.transform.position);
            meshAgent.speed = speed;
            meshAgent.stoppingDistance = distanceToFollow;      
            
        }
        else
        {
            //meshAgent.SetDestination(transform.position); // this should set it to the current spot and let it be for life. 
            HandleIdle();
        }

        // todo animation 
        //animation?.SetBool("FollowPlayer", true);        
    }
}
