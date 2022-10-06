using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedEnemy : DroppedItem
{
    public EnemyDrop item;
    public void Start()
    {
        Debug.Log("Dropped");
    }
    protected override void OnTriggerStay(Collider other)
    {
        if (other.name == "Player1" && Input.GetKey(KeyCode.LeftControl))
        {
            if (FindObjectOfType<Inventory>().HaveFreeSlot)
            {
                Inventory.PickUpItem.Invoke(item.GetComponent<Slot>());
                Destroy(gameObject);
            }
        }
    }
}
