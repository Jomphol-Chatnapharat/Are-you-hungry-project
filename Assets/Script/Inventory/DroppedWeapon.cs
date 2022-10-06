using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedWeapon : DroppedItem
{
    [SerializeField] public Weapon item;

    protected override void OnTriggerStay(Collider other)
    {
        if (other.name == "Player1" && Input.GetKey(KeyCode.LeftControl))
        {
            if (FindObjectOfType<Inventory>().HaveFreeSlot)
            {
                item.GetComponent<WeaponItem>().giveBackOnDestroy = true;
                Inventory.PickUpItem.Invoke(item.GetComponent<Slot>());
                Destroy(gameObject);
            }
        }
    }
}
