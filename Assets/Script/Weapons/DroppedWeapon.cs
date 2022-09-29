using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{
    [SerializeField] public Weapon weap;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player1")
        {
            Inventory inv = other.gameObject.GetComponentInChildren<Inventory>();
            inv.AddWeapon(weap);
            inv.EquipWeapon();
            Destroy(gameObject);
        }
    }
}
