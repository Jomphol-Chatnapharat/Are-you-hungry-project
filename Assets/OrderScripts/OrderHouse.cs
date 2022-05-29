using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderHouse : MonoBehaviour
{

    public int HouseId;
    OrderManager _manager;

    void Start()
    {
        _manager = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag == "Enemy")
        {
            if (_manager.ItemTrue(col.gameObject.GetComponent<SimpleEnemy>().referenceItem,HouseId))
            {
                _manager.AddAndCheckOrder(col.gameObject.GetComponent<SimpleEnemy>().referenceItem);
                Destroy(col.gameObject);
            }
            
        }
    }

}
