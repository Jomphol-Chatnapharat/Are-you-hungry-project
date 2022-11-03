using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static System.Action<Slot> PickUpItem;
    [SerializeField] private List<InventorySlot> slots;
    [SerializeField] private List<Weapon> weaponPrefabs;
    [SerializeField] private GameObject weaponHolder;
    private InventorySlot currentlyEquipped;

    public bool HaveFreeSlot
    {
        get { if (GetFreeSlot() == null) return false;
            else return true;
        }
    }

    public void Start()
    {
        currentlyEquipped = slots[0];
        EquipItem(currentlyEquipped);
        PickUpItem += PickUp;
    }
    public void Update()
    {
        SWInventoryUpdate();
    }
    public void PickUp(Slot item)
    {
        InventorySlot slot = CheckForSameItem(item);
        if (slot != null)
        {
            Slot tempItem = Instantiate(item, weaponHolder.transform);
            slot.AddItem(tempItem);
            EquipItem(slot);
        }
        else
        {
            slot = GetFreeSlot();
            if (slot != null)
            {
                Slot tempItem = Instantiate(item, weaponHolder.transform);
                slot.AddItem(tempItem);
                EquipItem(slot);
            }          
        }
            

    }
    public InventorySlot CheckForSameItem(Slot item)
    {
        if (item != null && item.GetComponent<Weapon>() != null)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.itemInSlot != null && slot.itemInSlot.TryGetComponent<Weapon>(out Weapon weap))
                {
                    if (slot.itemInSlot.GetComponent<Weapon>().GetType().ToString() == item.GetComponent<Weapon>().GetType().ToString()) return slot;
                }
            }
        }
        return null;
    }
    public Weapon GetPrefab(Weapon weap)
    {
        foreach (Weapon weapon in  weaponPrefabs)
        {
            if (weap.GetType().ToString() == weapon.GetType().ToString())
            {
                return weapon;
            }
        }
        return null;
    }
    public void RemoveWeapon(InventorySlot slot)
    {
        slot.AddItem(null);
    }
    public InventorySlot GetFreeSlot()
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.itemInSlot == null)
            {
                return slot;
            }
        } 
        return null;
    }
    void EquipItem(InventorySlot item)
    {
        currentlyEquipped.UnEquipItem();
        item.EquipItem();
        currentlyEquipped = item;
    }
    public void EquipItem()
    {
        InventorySlot item = slots[0];
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
              if ( gameObject.GetComponent<invTest>().player.grabObj != null)
              {
                  gameObject.GetComponent<invTest>().player.grabObj.SetActive(false);
                  gameObject.GetComponent<invTest>().player.grabObj = null;
                  gameObject.GetComponent<invTest>().player.grabbedRB = null;
                    gameObject.GetComponent<invTest>().player.WeightBar.SetActive(false);
                    Debug.Log(gameObject.GetComponent<invTest>().currentWeight.ToString());


                }


                EquipItem(slots[0]);
              //  Debug.Log(slots[0].gameObject.name);
            }
            else
            {
                if (gameObject.GetComponent<invTest>().player.grabObj != null)
                {
                    gameObject.GetComponent<invTest>().player.grabObj.SetActive(false);
                    gameObject.GetComponent<invTest>().player.grabObj = null;
                    gameObject.GetComponent<invTest>().player.grabbedRB = null;
                    gameObject.GetComponent<invTest>().player.WeightBar.SetActive(false);
                    Debug.Log(gameObject.GetComponent<invTest>().currentWeight.ToString());

                }
                EquipItem(slots[slots.IndexOf(currentlyEquipped) + 1]);
              //  Debug.Log(slots[slots.IndexOf(currentlyEquipped) + 1].gameObject.name);
               // gameObject.GetComponent<invTest>().player.grabObj.SetActive(false);
               
            }
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            if (slots.IndexOf(currentlyEquipped) == 0)
            {
                if (gameObject.GetComponent<invTest>().player.grabObj != null)
                {
                    gameObject.GetComponent<invTest>().player.grabObj.SetActive(false);
                    gameObject.GetComponent<invTest>().player.grabObj = null;
                    gameObject.GetComponent<invTest>().player.grabbedRB = null;
                    gameObject.GetComponent<invTest>().player.WeightBar.SetActive(false);
                    Debug.Log(gameObject.GetComponent<invTest>().currentWeight.ToString());

                }
                EquipItem(slots[slots.Count - 1]);
               // Debug.Log(slots[slots.Count - 1].gameObject.name);

            }
            else
            {
                if (gameObject.GetComponent<invTest>().player.grabObj != null)
                {
                    gameObject.GetComponent<invTest>().player.grabObj.SetActive(false);
                    gameObject.GetComponent<invTest>().player.grabObj = null;
                    gameObject.GetComponent<invTest>().player.grabbedRB = null;
                    gameObject.GetComponent<invTest>().player.WeightBar.SetActive(false);

                    Debug.Log(gameObject.GetComponent<invTest>().currentWeight.ToString());
                }
                EquipItem(slots[slots.IndexOf(currentlyEquipped) - 1]);
              //  Debug.Log(slots[slots.IndexOf(currentlyEquipped) - 1].gameObject.name);

            }
        }
    }
}
