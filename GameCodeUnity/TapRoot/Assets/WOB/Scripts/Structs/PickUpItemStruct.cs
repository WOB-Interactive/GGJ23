using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewPickupItem", menuName ="TapRoot/Pickup Item")]
public class PickUpItemStruct : ScriptableObject
{
    
    [Range(10, 100)]
    public int pickupPoints = 10;    
    public GameObject PickupItemPrefab;

}
