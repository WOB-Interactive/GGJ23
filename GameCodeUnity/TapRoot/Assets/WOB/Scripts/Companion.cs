using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public enum CompanionStates
{
    Following,
    Find_Pickup,
    Item_Near,
    enemy_near_1,
    enemy_near_2,
    Hurt_1,
    Hurt_2,
    Healing,
    Dead,

}

public class Companion : MonoBehaviour
{

    public static event Action<int> CompanionHurt;
    public static event Action CompanionDead;
    public static event Action<int> CompanionHealing;
    public static event Action ItemFound;
    public static event Action<int> EnemyNear;

    int maxHealth = 5;
    [SerializeField]
    int health = 3;
    [SerializeField]
    CompanionStates currentState;
    [SerializeField]
    GameObject playerLoc;
    [SerializeField]
    [Range(0.2f, 10f)]
    float distanceToFollow = 5;


    [SerializeField]
    float speed = 5f;

    [Header("SOUND FX")]
    [SerializeField]
    string enemyNearSoundFX;
    [SerializeField]
    string hurtSoundFX;
    [SerializeField]
    string healSoundFX;
    [SerializeField]
    string deathSoundFX;


    Animator companionAnimation;
    NavMeshAgent meshAgent;
    bool isDead = false;



    private void OnEnable()
    {
        CompanionHurt += HandleCompanionHurt;
        CompanionHealing += HandleCompanionHeal;

        //EnemyNear += HandleEnemyNear;
        
    }

    private void OnDisable()
    {
        CompanionHurt -= HandleCompanionHurt;
        CompanionHealing -= HandleCompanionHeal;

        //EnemyNear -= HandleEnemyNear;
    }


    void HandleCompanionHurt(int amount) {
        currentState = CompanionStates.Hurt_1;
        HandleHurt(amount);
    }

    void HandleCompanionHeal(int amount)
    {
        currentState = CompanionStates.Healing;
        HandleHeal(amount);
    }

    void HandleEnemyNear(int level)
    {
        if (level > 1)
        {
            currentState = CompanionStates.enemy_near_2;
        }

        currentState = CompanionStates.enemy_near_1;

        HandleEnemyNearNotification(level);

    }

    // Start is called before the first frame update
    void Start()
    {
        //companionAnimation = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
    }

    #region Companion State Handler

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            switch (currentState)
            {
                case CompanionStates.Following:
                    HandleFollowPlayer();
                    ListenForEnemies();
                    break;
                case CompanionStates.Find_Pickup:
                    HandleFindPickup();
                    break;
            }
        }
    }

    void HandleFollowPlayer()
    {
        // update for traking player
        meshAgent.SetDestination(playerLoc.transform.position);

        // todo animation 
        //companionAnimation?.SetBool("FollowPlayer", true);        
    }


    void ListenForEnemies()
    {
        //todo this is another projection we want to see when we can prevent being noticed

        // this will call the enemy nearCheck
    }

    // This is how we will be detecting all items in our search field
    

    void HandleFindPickup() {
        
        //https://docs.unity3d.com/ScriptReference/Physics.SphereCast.html
        // Steps: Cast SphearRay in General directions
        // OnHit of Same Type Then Notify by animation.
        // If enough health able to track down item.


        // todo animation and tracking for searching
        //companionAnimation?.SetBool("FollowPlayer", false);

        //todo animation and notification for found
        ItemFound?.Invoke();
    }

    

    [ContextMenu("Execute Enemy")]
    void DebugEnemyCheck()
    {
        HandleEnemyNearNotification(2);
    }

    void HandleEnemyNearNotification(int level) {
        // here we update the companion player interaction
        //companionAnimation?.SetBool("FollowPlayer", true);
        //companionAnimation?.SetBool("EnemyNear", false);
        // new Animation/Color? 

        //Todo Apply Sound FX    

        EnemyNear?.Invoke(level);
        AudioManager.instance.PlaySound(enemyNearSoundFX);
    }


    [ContextMenu("Take Damage")]
    void TakeDamage()
    {
        HandleHurt(1);
    }

    void HandleHurt(int amount) {
        this.health -= amount;
        if(this.health <= 0)
        {
            this.health = 0;
            currentState = CompanionStates.Dead;
            CompanionDead?.Invoke();
            HandleDead();
        }

        //todo Apply Animation
        //companionAnimation?.SetTrigger("Hurt");
        //Todo Apply Sound FX
        AudioManager.instance.PlaySound(hurtSoundFX);


    }

    void HandleHeal(int amount) {
        this.health += amount; 
        if (this.health > maxHealth)
        {
            this.health = maxHealth;
        }

        //todo Apply Animation
        //companionAnimation?.SetTrigger("Heal");
        //Todo Apply Sound FX
        AudioManager.instance.PlaySound(healSoundFX);
    }

    void HandleDead() {
        AudioManager.instance.PlaySound(deathSoundFX);
        isDead = true;
        StartCoroutine("DisableCompanion");

    }

    IEnumerator DisableCompanion()
    {
        //todo Apply animation
        //companionAnimation?.SetBool("FollowPlayer", false);
        //companionAnimation?.SetTrigger("Dead");
        yield return new WaitForSeconds(3f); // death animation time

        gameObject.SetActive(false);

    }


    #endregion
}
