using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<InventorySlot> slots;
    [SerializeField] List<GameObject> weaponPrefabs;
    [SerializeField] GameObject weaponHolder;
    InventorySlot currentlyEquipped;

    public void Start()
    {
        currentlyEquipped = slots[0];
        InitWeapons();
        EquipWeapon(currentlyEquipped);
    }
    public void Update()
    {
        SWInventoryUpdate();
    }
    public void AddWeapon(Weapon weapon)
    {
        InventorySlot slot = GetFreeSlot();
        if (slot != null)
        {
            slot.ChangeWeapon(weapon);
        }
        else Debug.Log("Inventory is Full!");

    }     
    public void PickUpWeapon(string weaponType)
        {
            InventorySlot slot = GetFreeSlot();
            if (slot != null)
            {
                foreach(GameObject gameobj in weaponPrefabs)
            {
                Debug.Log(gameobj.GetComponent<Weapon>().GetType().ToString());
                if (gameobj.GetComponent<Weapon>().GetType().ToString() == weaponType)
                {
                    GameObject obj = Instantiate(gameobj, weaponHolder.transform);
                    AddWeapon((Weapon)obj.GetComponent(weaponType));
                    slot.weaponHold.Init();
                    slot.UnEquipItem();
                    break;
                }
            }
            }
            else Debug.Log("Inventory is Full!");
        }
    public void RemoveWeapon(InventorySlot slot)
    {
        slot.ChangeWeapon(null);
    }
    public InventorySlot GetFreeSlot()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.weaponHold == null)
            {
                return slot;
            }
        }
        return null;
    }
    void InitWeapons()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.weaponHold != null)
            {
               slot.weaponHold = Instantiate(slot.weaponHold.gameObject, weaponHolder.transform).GetComponent<Weapon>();
               slot.UnEquipItem();
            }
        }
    }
    void EquipWeapon(InventorySlot item)
    {
        currentlyEquipped.UnEquipItem();
        item.EquipItem();
        currentlyEquipped = item;
    }
    void SWInventoryUpdate()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            if (slots.Count-1 == slots.IndexOf(currentlyEquipped))
            {
                EquipWeapon(slots[0]);
            }
            else
            {
                EquipWeapon(slots[slots.IndexOf(currentlyEquipped) + 1]);
            }
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            if (slots.IndexOf(currentlyEquipped) == 0)
            {
                EquipWeapon(slots[slots.Count - 1]);
            }
           else
            {
                EquipWeapon(slots[slots.IndexOf(currentlyEquipped) - 1]);
            }
        }
    }
}
