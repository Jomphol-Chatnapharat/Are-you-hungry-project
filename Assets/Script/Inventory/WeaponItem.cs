using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Slot
{
    [SerializeField] private Weapon weapon;
    public bool giveBackOnDestroy = true;
    public override void Start()
    {
        itemType = InventoryItemType.Weapon;
        
    }
    public override void DropItem()
    {
            DroppedWeapon droppedItem = (DroppedWeapon)Instantiate(objToDrop, Camera.main.transform.position + Camera.main.transform.forward * 3, Quaternion.identity);
            droppedItem.item = FindObjectOfType<Inventory>().GetPrefab(weapon);
            giveBackOnDestroy = false;
            Destroy(gameObject);
    }
    public override void UseItem()
    {

    }
    public override void Equip()
    {
        gameObject.SetActive(true);
    }
    public override void UnEquip()
    {
        gameObject.SetActive(false);
    }
    public void OnDestroy()
    {
        if (giveBackOnDestroy) Inventory.PickUpItem(FindObjectOfType<Inventory>()?.GetPrefab(weapon).GetComponent<Slot>());
    }
}
