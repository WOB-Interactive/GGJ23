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
    [SerializeField]
    GameObject ItemFoundFX;
    [SerializeField]
    [Range(5f, 60f)]
    float itemFoundTimeLimit = 10f;


    private void Awake()
    {

        if (item)
        {
            Instantiate(item.PickupItemPrefab, itemModelAssetLocation.transform);
        }
            PickupItemElement.PickedUp += HandlePickup;
    }

    public void OnItemFoundHandler()
    {
        ItemFoundFX.SetActive(true);
        StartCoroutine("ClearItemFound");
    }

    IEnumerator ClearItemFound()
    {
        yield return new WaitForSeconds(itemFoundTimeLimit);
        ItemFoundFX.SetActive(false);
    }

    public void HandlePickup()
    {
        OnItemPickup?.Invoke(item);
        FX?.Play(true);

        if (pickupSoundFX != String.Empty)
        {
            AudioManager.instance.PlaySound(pickupSoundFX);            
        }
        gameObject.SetActive(false);
        Destroy(gameObject, objectDestroyLifetime);
    }

}
