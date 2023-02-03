using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour
{
    [SerializeField]
    GameObject itemReceived;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemInteraction(float animationDelay)
    {
        StartCoroutine("DropItem", animationDelay);
    }

    IEnumerator DropItem(float animationDelay)
    {
        yield return new WaitForSeconds(animationDelay);
        Instantiate(itemReceived, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }



}
