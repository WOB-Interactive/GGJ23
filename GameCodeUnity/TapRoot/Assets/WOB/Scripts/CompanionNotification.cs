using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CompanionNotification : MonoBehaviour
{
    public static event Action<string> ShowClosedCaption;

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
    GameObject targetPlayer;

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
        ShowClosedCaption?.Invoke("[Companion]: There's an Item near by");
    }

    private void Update()
    {

        transform.LookAt(targetPlayer.transform);
    }

    void OnFullDayCycleCompleteHandler()
    {
        NotifyHandler(fullDayBackHomeImage);
        ShowClosedCaption?.Invoke("[Companion]: Wow That's a day back home");
    }

    void OnEnemyNear(int level)
    {

        ShowClosedCaption?.Invoke("[Companion]: There's an enemy near by");
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