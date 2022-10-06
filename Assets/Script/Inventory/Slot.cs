using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryItemType { Weapon, Consumable, EnemyDrop}

public class Slot : MonoBehaviour
{
    [SerializeField] protected Sprite icon;
    [SerializeField] protected DroppedItem objToDrop;
    [SerializeField] protected InventoryItemType itemType;

    public InventoryItemType ItemType { get => itemType; }

    public virtual void Start()
    {
    }
    public virtual void DropItem()
    {

    }
    public virtual void UseItem()
    {

    }
    public Sprite GetIcon => icon;

    public virtual void Equip()
    {
        gameObject.SetActive(true);
    }
    public virtual void UnEquip()
    {
        gameObject.SetActive(false);
    }
}
