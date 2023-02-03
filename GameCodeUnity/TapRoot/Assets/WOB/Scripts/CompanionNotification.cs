using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CompanionNotification : MonoBehaviour
{
    [SerializeField]
    [Range(1.5f, 30f)]
    float notificationDisplayTime = 5f;
    [SerializeField]
    Image notificationImage;
    

    Animator animator;
    [SerializeField]
    bool displayNotification = false;

    [SerializeField]
    Sprite itemFound;

    [SerializeField]
    Sprite Level1EnemyWarning;

    [SerializeField]
    Sprite Level2EnemyWarning;

    [SerializeField]
    Sprite fullDayBackHomeImage;

    [SerializeField]
    Camera targetPlayer;

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void OnEnable()
    {
        Companion.EnemyNear += OnEnemyNear;
        SearchPower.ItemFound += OnItemFound;
        DayNightTracker.OnFullDayCycleComplete += OnFullDayCycleCompleteHandler;
    }

    private void OnDisable()
    {
        Companion.EnemyNear -= OnEnemyNear;
        SearchPower.ItemFound -= OnItemFound;
        DayNightTracker.OnFullDayCycleComplete -= OnFullDayCycleCompleteHandler;
    }

    void OnItemFound(PIckupItem item)
    {
        NotifyHandler(itemFound);
        // here we can add an Improved power up to allow details about how many items found OR which specific Item. 
    }

    void OnFullDayCycleCompleteHandler()
    {
        NotifyHandler(fullDayBackHomeImage);
    }

    void OnEnemyNear(int level)
    {
        switch (level)
        {
            case 1:
                NotifyHandler(Level1EnemyWarning);
                break;

            case 2:
                NotifyHandler(Level2EnemyWarning);
                break;
        }
    }

    public void NotifyHandler(Sprite imageToDisplay)
    {
        transform.LookAt(targetPlayer.transform);
        displayNotification = true;
        notificationImage.sprite = imageToDisplay;
        animator.SetBool("Notify", displayNotification);
        StartCoroutine("RemoveNotice");
    }

    IEnumerator RemoveNotice()
    {
        displayNotification = false;
        yield return new WaitForSeconds(notificationDisplayTime);
        notificationImage.sprite = null;
        animator.SetBool("Notify", displayNotification);
    }
}