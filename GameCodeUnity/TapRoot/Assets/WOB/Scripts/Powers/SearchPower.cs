using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SearchPower : MonoBehaviour
{
    public static event Action ItemFound;
    public static event Action EnemyFound;

    float maxRangeRadius = 10;
    float resetRadius = 0.2f;
    [SerializeField]
    bool searching = false;
    [Range(1, 100)]
    int increments = 1;
    float tScale = 6;

    [SerializeField]
    float scanRadius = 2;
    


    private void OnEnable()
    {

        Companion.Search += OnSearchHandler;
    }

    private void OnDisable()
    {

        Companion.Search -= OnSearchHandler;
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    [SerializeField]
    Vector3 searchOffset = new Vector3();
    [SerializeField] Vector3 searchRotationOffset = new Vector3();
    // Update is called once per frame
    void Update()
    {
        if (searching)
        {
            target.Clear();
            float degrees = 0;

            float scale = (gameObject.transform.localScale.x + increments * Time.deltaTime * tScale);
            if (scale > maxRangeRadius)
            {
                scale = resetRadius;
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


    public List<Vector3> target;

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