using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIckupItem : MonoBehaviour
{

    public static event Action<PickUpItemStruct>  OnItemPickup;

    [SerializeField]
    ParticleSystem FX;
    [SerializeField]
    string pickupSoundFX;
    [SerializeField]
    [Range(0f, 1.0f)]
    float pickupAudioVolume = 0.5f;
    [Range(0f, 3.0f)]
    [Tooltip("This will be how long the item will last before it's fully destroyed")]
    float objectDestroyLifetime = 10f;

    [SerializeField]
    PickUpItemStruct item;

    [SerializeField]
    GameObject itemModelAssetLocation;


    private void Awake()
    {
        Instantiate(item.PickupItemPrefab, itemModelAssetLocation.transform);
        PickupItemElement.PickedUp += HandlePickup;
    }

    public void HandlePickup()
    {
        OnItemPickup?.Invoke(item);
        FX?.Play(true);

        if (pickupSoundFX != String.Empty)
        {
            AudioManager.instance.PlaySound(pickupSoundFX);            
        }       
        
        Destroy(gameObject, objectDestroyLifetime);
        

    }

    private void OnDestroy()
    {
        Debug.LogError("This was called why?");
    }
}
