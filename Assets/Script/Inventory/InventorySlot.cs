using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{   
    public Slot itemInSlot;
    private bool isDropped = false;
    private Image icon;
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

}
