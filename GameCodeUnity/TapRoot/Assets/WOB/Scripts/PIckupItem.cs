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
    [Tooltip("Sound to Play when Picked Up")]
    string pickupSoundFX;
    [Range(0f, 5.0f)]
    [Tooltip("This will be how long the item will last before it's fully destroyed")]
    float objectDestroyLifetime = 5f;

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

}
