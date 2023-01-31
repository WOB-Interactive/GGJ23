using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    int score = 0;
    int threatLevel = 0;



    private void OnEnable()
    {
        PIckupItem.OnItemPickup += OnItemPickupHandler;
    }

    private void OnDisable()
    {
        PIckupItem.OnItemPickup -= OnItemPickupHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnItemPickupHandler(PickUpItemStruct item)
    {
        score += item.pickupPoints;
    }
}
