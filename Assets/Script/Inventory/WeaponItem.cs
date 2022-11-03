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
        if(gameObject.GetComponent<invTest>() != null && gameObject.GetComponent<invTest>().player.grabbedRB == null)
        {
            gameObject.GetComponent<invTest>().player.grabbedRB = gameObject.GetComponent<Rigidbody>();
            gameObject.GetComponent<invTest>().player.grabObj = gameObject;
            gameObject.GetComponent<invTest>().player.WeightBar.SetActive(true);

        }

    }
    public override void UnEquip()
    {
        gameObject.SetActive(false);
        if (gameObject.GetComponent<invTest>() != null && gameObject.GetComponent<invTest>().player.grabbedRB != null)
        {
            gameObject.GetComponent<invTest>().player.grabbedRB = null;
            gameObject.GetComponent<invTest>().player.grabObj = null;
          
            //Debug.Log(gameObject.GetComponent<invTest>().currentWeight);
            gameObject.GetComponent<invTest>().player.WeightBar.SetActive(false);

        }
    }
    public void OnDestroy()
    {
        if (giveBackOnDestroy) Inventory.PickUpItem(FindObjectOfType<Inventory>()?.GetPrefab(weapon).GetComponent<Slot>());
    }
        

}
