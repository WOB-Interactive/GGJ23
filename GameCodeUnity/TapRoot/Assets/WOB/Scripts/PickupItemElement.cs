using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PickupItemElement : MonoBehaviour
{
    public static event Action PickedUp;
    PIckupItem item;    

    private void Awake()
    {
        item = GetComponentInParent<PIckupItem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            item.HandlePickup();
            this.gameObject.SetActive(false);
        }
    }
}
