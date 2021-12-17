using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGetItem : MonoBehaviour
{
    [SerializeField] private GameObject itemFixPosition;

    private Interface_Item interface_item;

    void Start()
    {
       // itemFixPosition.crea
        itemFixPosition.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Item"))
        {

           
            Destroy(other.gameObject);

        }
    }

    private void GetItem(Item.ItemType itemType)
    {
        interface_item.GotItem(itemType);

    }
}
