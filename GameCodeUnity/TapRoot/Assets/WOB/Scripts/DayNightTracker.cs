using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DayNightTracker : MonoBehaviour
{
    public static event Action OnFullDayCycleComplete;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FullCycleComplete()
    {
        Storage.AddDaysSearching(1);
        OnFullDayCycleComplete?.Invoke();
    }
}
