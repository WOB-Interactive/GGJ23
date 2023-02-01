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

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void OnEnable()
    {
        Companion.EnemyNear += OnEnemyNear;
        Companion.ItemFound += OnItemFound;
    }

    private void OnDisable()
    {
        Companion.EnemyNear -= OnEnemyNear;
        Companion.ItemFound -= OnItemFound;
    }

    void OnItemFound()
    {
        NotifyHandler(itemFound);
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
