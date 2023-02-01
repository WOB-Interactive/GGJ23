using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SearchPowers : MonoBehaviour
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
    
        // Update is called once per frame
    void Update()
    {
        if(searching)
        {
            float scale = (gameObject.transform.localScale.x + increments * Time.deltaTime * tScale);
            if(scale > maxRangeRadius)
            {
                
                scale = resetRadius;
                SearchDirection(transform.forward);
                SearchDirection(transform.up);
                transform.RotateAround(transform.position, Vector3.up, 90);
                SearchDirection(transform.forward);
                transform.RotateAround(transform.position, Vector3.up, 90);
                SearchDirection(transform.forward);
                transform.RotateAround(transform.position, Vector3.up, 90);
                SearchDirection(transform.forward);
                transform.RotateAround(transform.position, Vector3.up, 90); // back To Normal
                searching = false;
            }            
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    void SearchDirection(Vector3 direction)
    {
        RaycastHit hit;
        // we want to cast the sphere at the end of the animation due to the discovery rate
        if (Physics.SphereCast(transform.position, 2, direction, out hit, 2))
        {

            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.collider.gameObject.GetComponent<PickupItemElement>() != null)
            {
                ItemFound?.Invoke();
            }


            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                EnemyFound?.Invoke();
            }
        } else
        {
            Debug.Log("No Hits");
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5, Color.white);
        }
    }

    void OnSearchHandler()
    {
        searching = true;

    }

}
