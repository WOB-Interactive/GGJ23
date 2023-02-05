using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class InventorySlot
{
    
    public PickUpItemStruct item;
    [SerializeField]
    int count =0;
    [SerializeField]
    Sprite displayImage;

    InventorySlot(PickUpItemStruct itemIn)
    {
        item = itemIn;
    }

    public void AddItem (int cnt)
    {
        count += cnt;
    }

    public int GetCount() { return count; }

    public Sprite GetDisplayImage()
    {
        return displayImage;
    }

}





public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    InventorySlot carrots;
    [SerializeField]
    InventorySlot turnips;
    [SerializeField]
    InventorySlot radish;
    [SerializeField]
    InventorySlot beets;

    [SerializeField]
    InventorySlot skull;
    [SerializeField]
    InventorySlot leftEye;
    [SerializeField]
    InventorySlot rightEye;


    [Header("UI")]
    [SerializeField]
    InventoryDisplay carrotsDisplay;
    [SerializeField]
    InventoryDisplay turnipsDisplay;
    [SerializeField]
    InventoryDisplay radishDisplay;
    [SerializeField]
    InventoryDisplay beetsDisplay;

    // todo 
    InventoryDisplay collectables;


    // Start is called before the first frame update
    void Start()
    {
        carrotsDisplay.Setup(carrots.GetDisplayImage(), 0);
        turnipsDisplay.Setup(turnips.GetDisplayImage(), 0);
        radishDisplay.Setup(radish.GetDisplayImage(), 0);
        beetsDisplay.Setup(beets.GetDisplayImage(), 0);


        PIckupItem.OnItemPickup += OnItemPickupHandler;
    }

    public void OnItemPickupHandler (PickUpItemStruct itemStruct)
    {
        if (itemStruct.Equals(carrots.item)) {
            carrots.AddItem(1);
            carrotsDisplay.SetCount(carrots.GetCount());
        }

        if (itemStruct.Equals(turnips.item))
        {
            turnips.AddItem(1);
            turnipsDisplay.SetCount(turnips.GetCount());

        }

        if (itemStruct.Equals(radish.item))
        {
            radish.AddItem(1);
            radishDisplay.SetCount(radish.GetCount());

        }

        if (itemStruct.Equals(beets.item))
        {
            beets.AddItem(1);
            beetsDisplay.SetCount(beets.GetCount());

        }
    }

}
