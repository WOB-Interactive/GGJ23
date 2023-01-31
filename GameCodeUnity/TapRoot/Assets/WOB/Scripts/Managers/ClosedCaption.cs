using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClosedCaption : MonoBehaviour
{
    TMP_Text textDisplay;
    Image panelDisplay;
    bool captionsEnabled;


    private void OnEnable()
    {
        AudioManager.closedCaption += DisplayCaption;        
    }

    private void OnDisable()
    {
        AudioManager.closedCaption -= DisplayCaption;
    }

    // Start is called before the first frame update
    void Start()
    {
        panelDisplay = GetComponent<Image>();
        textDisplay = GetComponentInChildren<TMP_Text>();
        captionsEnabled = Storage.GetClosedCaptionEnabled();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayCaption(string text)
    {
        if (captionsEnabled)
        {
            StartCoroutine(DisplayCaptionAnimation(text, 5));
        }
    }

    private void Reset()
    {
        panelDisplay.enabled = false;
        textDisplay.SetText("");        
    }


    IEnumerator DisplayCaptionAnimation(string text, float displayTime)
    {
        panelDisplay.enabled = true;
        textDisplay.SetText(text);
        yield return new WaitForSeconds(displayTime);

        Reset();
    }
}
