using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    TMP_Text countDisplay;
    [SerializeField]
    Image displayImage;


    public void SetCount(int count) {
        countDisplay.SetText(String.Format("{0}", count));
    }

    public void Setup(Sprite displayImageIn, int count)
    {
        displayImage.sprite = displayImageIn;
        countDisplay.SetText(String.Format("{0}", count));
    }

}
