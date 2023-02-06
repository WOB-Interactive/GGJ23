using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExitWorld : MonoBehaviour
{

    public static event Action GetOutOfHere;

    private void OnTriggerEnter(Collider other)
    {
        GetOutOfHere?.Invoke();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GetOutOfHere?.Invoke();
    }
}
