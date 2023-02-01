using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyNear : MonoBehaviour
{
    public static event Action<Transform> ItemToFollow;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Companion>())
        {
            ItemToFollow?.Invoke(other.gameObject.transform);
        } 
        else if(other.CompareTag("Player"))
        {
           // ItemToFollow?.Invoke(other.gameObject.transform);
        }
    }

}
