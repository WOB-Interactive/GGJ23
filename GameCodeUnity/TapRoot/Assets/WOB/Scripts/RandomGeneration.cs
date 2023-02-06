using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    public static RandomGeneration SharedInstance;


    public List<GameObject> pickupItems;
    public List<GameObject> enemyItems;    

    public GameObject[] pickUpItemsSource;
    public int copiesOfItems;

    

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        // pickup items
        pickupItems = new List<GameObject>();
        GameObject tmp;
        
        for (int j = 0; j < pickUpItemsSource.Length; j++)
        {
            GameObject objectToPool = pickUpItemsSource[j];
            for (int i = 0; i < copiesOfItems; i++)
            {
                tmp = Instantiate(objectToPool);
                tmp.SetActive(false);
                pickupItems.Add(tmp);
            }
        }
    }
    public GameObject GetPickUpItem()
    {
        for (int i = 0; i < pickupItems.Count; i++)
        {
            if (!pickupItems[i].activeInHierarchy)
            {
                return pickupItems[i];
            }
        }
        return null;
    }
}
