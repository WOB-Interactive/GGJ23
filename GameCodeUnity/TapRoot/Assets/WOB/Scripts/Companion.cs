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
    public static event Action<int> EnemyNear;
    public static event Action Search;

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
    float searchFieldRadius = 5;
    [SerializeField]
    GameObject searchField;


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
    bool searching = false;
    bool canMove = false;



    private void OnEnable()
    {
        CompanionHurt += HandleCompanionHurt;
        CompanionHealing += HandleCompanionHeal;
        SearchPower.EnemyFound += OnEnemyFound;
        SearchPower.ItemFound += OnItemFound;
        GameManager.GameStateChange += OnGameStateChangeHandler;


    }

    private void OnDisable()
    {
        CompanionHurt -= HandleCompanionHurt;
        CompanionHealing -= HandleCompanionHeal;
        SearchPower.EnemyFound -= OnEnemyFound;
        SearchPower.ItemFound -= OnItemFound;
        GameManager.GameStateChange -= OnGameStateChangeHandler;
    }



    void OnGameStateChangeHandler(GameStates state)
    {
        switch (state)
        {
            case GameStates.Play:
                // MOVEABLE
                canMove = true;
                break;
            default:
                //not moving
                canMove = false;
                break;
        }
    }


    void HandleCompanionHurt(int amount)
    {
        currentState = CompanionStates.Hurt_1;
        HandleHurt(amount);
    }

    void HandleCompanionHeal(int amount)
    {
        currentState = CompanionStates.Healing;
        HandleHeal(amount);
    }

    void OnItemFound()
    {
        searching = false;
    }

    void OnEnemyFound()
    {
        HandleEnemyNear(1);
        searching = false;
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
        if (!isDead && canMove)
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



    IEnumerator ManageSearchStates()
    {
        if (!searching)
        {
            searching = true;
            Search?.Invoke();
        }
        yield return new WaitForSeconds(1);
        searching = false;
        currentState = CompanionStates.Following;
    }

    void HandleFindPickup()
    {

        // todo animation 
        //companionAnimation?.SetBool("FollowPlayer", false);
        StartCoroutine(ManageSearchStates());


    }



    [ContextMenu("Execute Enemy")]
    void DebugEnemyCheck()
    {
        HandleEnemyNearNotification(2);
    }

    void HandleEnemyNearNotification(int level)
    {
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
        // this will be used with a damage multiplier based on enemy types
        HandleHurt(1);
    }

    void HandleHurt(int amount)
    {
        this.health -= amount;
        if (this.health <= 0)
        {
            this.health = 0;
            currentState = CompanionStates.Dead;            
            HandleDead();
            CompanionDead?.Invoke();
        }

        //todo Apply Animation
        //companionAnimation?.SetTrigger("Hurt");
        //Todo Apply Sound FX
        AudioManager.instance.PlaySound(hurtSoundFX);


    }

    void HandleHeal(int amount)
    {
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

    void HandleDead()
    {
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

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }
    }


    #endregion
}