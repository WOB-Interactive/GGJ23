using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum PlayerStates
{
    Alive,
    Digging, // needed for initial interactions
    Dead
}

public class PlayerFeatures : MonoBehaviour
{

    InteractItem currentInteract;

    public static event Action OnPlayerDeath;
    public static event Action<InteractItem> OnInteractiveAllowed;
    public static event Action OnNoInteraction;

    PlayerStates currentState = PlayerStates.Alive; // used for another reason you'll see

    private void OnCollisionEnter(Collision collision)
    {
        handleCollision(collision.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        handleCollision(other.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Interactive"))
        {
            HandleUncollision();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        handleCollision(other.gameObject);
    }


    private void handleCollision(GameObject collision)
    {
        //todo Add Player death animation
        if (collision.CompareTag("Enemy"))
        {
            OnPlayerDeath?.Invoke();
        }

        if(collision.CompareTag("Interactive")) {
            currentInteract = collision.GetComponent<InteractItem>();
            OnInteractiveAllowed?.Invoke(currentInteract);
        }

        if (collision.CompareTag("Pickup") && collision.gameObject.activeSelf)
        {
           collision.gameObject.GetComponent<PIckupItem>().HandlePickup();
        }
    }

    private void HandleUncollision()
    {
        currentInteract = null;
        OnNoInteraction?.Invoke();
    }




    public void PlayerKilled()
    {
        currentState = PlayerStates.Dead;
        OnPlayerDeath?.Invoke();
    }


    private void OnEnable()
    {
        StarterAssets.StarterAssetsInputs.OnInteractPressed += OnInteractPressedHandler;
    }

    private void OnDisable()
    {

        StarterAssets.StarterAssetsInputs.OnInteractPressed -= OnInteractPressedHandler;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnInteractPressedHandler()
    {
        if (currentInteract)
        {
            // here we process the interaction
            currentInteract.ItemInteraction(3); // we can extend this a bit further
            
        }
    }

}
