using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Slot itemInSlot;
    private bool isDropped = false;
    private Image icon;
    public InventoryData Monstr;
    public PlayerBehavior player;
    public int _id;
    private void Start()
    {
        icon = GetComponent<Image>();
        if (itemInSlot != null) ChangeIcon(itemInSlot.GetIcon);
    }
    private void Update()
    {
        if (itemInSlot != null && itemInSlot.gameObject.activeSelf && Input.GetKeyDown(KeyCode.G))
        {
            itemInSlot.DropItem();
            icon.sprite = null;
            isDropped = true;
        }
    }
    public void AddItem(Slot item)
    {
        if (item != null)
        {
            itemInSlot = item;
            icon.sprite = item.GetIcon;
        }
        else
        {
            itemInSlot = null;
            icon.sprite = null;
        }
    }
    public void ChangeIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
    public void EquipItem()
    {
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1f);
        if (itemInSlot != null)
        {
            itemInSlot.Equip();
        }
    }
    public void UnEquipItem()
    {
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f);
        if (itemInSlot != null)
        {
            itemInSlot.UnEquip();
        }
    }
    public void DropMonster()
    {
        if (player.grabObj == null && player.grabbedRB == null && player.boxObj.Inventory[_id] != null )
        {
            GameObject obj = Instantiate(Monstr.prefab, player.transform.position, player.transform.rotation);

            player.grabbedRB = obj.GetComponent<Collider>().gameObject.GetComponent<Rigidbody>();
            if (player.grabbedRB)
            {

                player.HoldingObj();
                Debug.Log('y');
                icon.sprite = null;
                player.boxObj.currentWeight -= player.boxObj.Inventory[_id].Weight;
                player.boxObj.Inventory[_id] = null;
                player.CheckInv();
            }
        }
    }
}

