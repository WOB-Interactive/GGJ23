using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIckupItem : MonoBehaviour
{

    [SerializeField]
    ParticleSystem FX;
    [SerializeField]
    AudioClip pickupSoundFX;
    [SerializeField]
    [Range(0f, 1.0f)]
    float pickupAudioVolume = 0.5f;
    [Range(0f, 3.0f)]
    [Tooltip("This will be how long the item will last before it's fully destroyed")]
    float objectDestroyLifetime = 0.5f;

    [Range(10, 100)]
    int pickupPoints = 10;

    GameObject player;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            Debug.Log("Player Handle Pickup");
            // TODO  Add Either Event Handler To manage Item pickup or utilize function within PlayerGameModel
            // player.getComponent<TapRootPlayer>().handlePickup(points); 
            // Prefferred >> to carry every possible pickup item.  
            // platyer.getComponent<TapRootPlayer>().HandlePickup(PickUpItemStruct);
            HandlePickup();
        }
    }


    void HandlePickup()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject.GetComponentInParent<Transform>().gameObject, objectDestroyLifetime);

        if (FX)
        {
            FX.Play(true);
        }

        if (pickupSoundFX)
        {
            // TODO convert to Closed Caption Pattern. 
            // ClosedCaption.PlayClip(SoundFXStruct) // This module will contain a reference to the player and the struct will maintain auido volume, clip, title and close caption text.
            AudioSource.PlayClipAtPoint(pickupSoundFX, transform.TransformPoint(player.transform.position), pickupAudioVolume);
        }




    }
}
