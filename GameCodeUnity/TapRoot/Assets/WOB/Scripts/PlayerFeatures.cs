using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum PlayerStates
{
    Alive,
    Dead
}

public class PlayerFeatures : MonoBehaviour
{

    public static event Action OnPlayerDeath;
    PlayerStates currentState = PlayerStates.Alive;

    private void OnCollisionEnter(Collision collision)
    {
        handleCollision(collision.gameObject);
    }

    private void handleCollision(GameObject collision)
    {

        //todo Add Player death animation
        if (collision.CompareTag("Enemy"))
        {
            OnPlayerDeath?.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        handleCollision(other.gameObject);
    }

    public void PlayerKilled()
    {
        currentState = PlayerStates.Dead;
        OnPlayerDeath?.Invoke();
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
