using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SearchPower : MonoBehaviour
{
    public static event Action ItemFound;
    public static event Action EnemyFound;
    

    [SerializeField]
    [Range(1, 10)]
    float maxRangeRadius = 10f;
    [SerializeField]
    bool searching = false;
    [Range(1, 10)]
    int increments = 1;
    [SerializeField]
    [Range(1, 10)]
    float scanRadius = 2; // there's a special pickup item that will increase the default scan radius

    [Header("Debug tuning only")]
    [SerializeField]
    Vector3 searchOffset = new Vector3();
    [SerializeField] Vector3 searchRotationOffset = new Vector3();

    float tScale = 6;
    public List<Vector3> target;
    MeshRenderer powerVisible;

    private void OnEnable()
    {

        Companion.Search += OnSearchHandler;
    }

    private void OnDisable()
    {

        Companion.Search -= OnSearchHandler;
    }

    /// <summary>
    /// handles when we find a power up to improve companion search powers. 
    /// </summary>
    /// <param name="amount"></param>
    void OnHandlePowerIncrease(int amount)
    {
        this.scanRadius += amount;
        if (scanRadius > maxRangeRadius) scanRadius = maxRangeRadius;
    }

    // Start is called before the first frame update
    void Start()
    {
        powerVisible = GetComponent<MeshRenderer>();
        powerVisible.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        powerVisible.enabled = searching;
        if (searching)
        {
            target.Clear();
            float degrees = 0;

            float scale = (gameObject.transform.localScale.x + increments * Time.deltaTime * tScale);
            if (scale > maxRangeRadius)
            {
                scale = scanRadius;
                SearchDirection(Vector3.up);
                while (degrees < 360)
                {
                    gameObject.transform.RotateAround(transform.position, Vector3.up + searchRotationOffset, degrees);                    
                    SearchDirection(Vector3.forward + searchOffset);
                    degrees += 45;
                }
                searching = false;

            }
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);

        foreach (Vector3 targetItem in target)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, targetItem);
        }
    }


    void SearchDirection(Vector3 direction)
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 2, direction, scanRadius, LayerMask.NameToLayer("Item") ,QueryTriggerInteraction.Collide );

        if (hits.Length > 0)
        {
            foreach(RaycastHit hit in hits)
            {
                target.Add(hit.collider.gameObject.transform.position);
                if (hit.collider.gameObject.GetComponent<PIckupItem>())
                {
                    ItemFound?.Invoke();
                }

                if(hit.collider.gameObject.CompareTag("Enemy"))
                {
                    EnemyFound?.Invoke();
                }
            }
        }
        else
        {

        }
        
    }

    void OnSearchHandler()
    {
        searching = true;

    }

}