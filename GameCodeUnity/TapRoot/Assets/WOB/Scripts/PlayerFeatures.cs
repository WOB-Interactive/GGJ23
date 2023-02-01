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

    private void OnCollisionEnter(Collision collision)
    {
        //todo Add Player death animation
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnPlayerDeath?.Invoke();
        }
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
