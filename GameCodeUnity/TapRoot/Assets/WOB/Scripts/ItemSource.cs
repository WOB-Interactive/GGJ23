using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSource : MonoBehaviour
{


    public float replenishTime = 15f;
    GameObject item;     



    // Start is called before the first frame update
    void Start()
    {
        GetPickup();
    }

    void GetPickup()
    {
        item = RandomGeneration.SharedInstance.GetPickUpItem();
        if (item)
        {
            item.transform.position = gameObject.transform.position;
            item.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
