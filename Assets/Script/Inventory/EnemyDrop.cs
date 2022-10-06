using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : Slot
{
    public InventoryData itemData;
    public override void Start()
    {
        itemType = InventoryItemType.EnemyDrop;
    }
    public override void DropItem()
    {
        DroppedEnemy droppedItem = (DroppedEnemy)Instantiate(objToDrop, Camera.main.transform.position + Camera.main.transform.forward * 3, Quaternion.identity);
        Destroy(gameObject);
    }
}
