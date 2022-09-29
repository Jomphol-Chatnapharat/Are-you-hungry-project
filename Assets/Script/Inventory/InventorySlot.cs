using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] Weapon weapPrefab;
    public Image icon;
    public Weapon weaponHold;
    public bool isDropped = false;
    [SerializeField] GameObject weaponHolder;

    private void Start()
    {
        if (weaponHold != null)
        {
            ChangeIcon(weaponHold.GetIcon);
        }
    }
    private void Update()
    {
        if (weaponHold != null && weaponHold.gameObject.activeSelf && Input.GetKeyDown(KeyCode.G))
        {
            DropWeapon();
            isDropped = true;
        }
        if (weaponHold == null && weapPrefab != null && !isDropped)
        {           
         weaponHold = Instantiate(weapPrefab.gameObject, weaponHolder.transform).GetComponent<Weapon>();
         EquipItem();           
        }
    }
    protected void DropWeapon()
    {
        weaponHold.gameObject.SetActive(false);
        GameObject obj = Instantiate(weaponHold.objToDrop, Camera.main.transform.position + Camera.main.transform.forward*3, Quaternion.identity);
        obj.GetComponent<DroppedWeapon>().weap = weapPrefab;
        Destroy(weaponHold.gameObject);
        icon.sprite = null;
    }
    public void ChangeWeapon(Weapon weapon)
    {
        if (weapon != null)
        {
            weaponHold = weapon;
            icon.sprite = weapon.GetIcon;
            if (isDropped)
            {
                weaponHold = Instantiate(weapPrefab.gameObject, weaponHolder.transform).GetComponent<Weapon>();
                weaponHold.gameObject.SetActive(false);
            }
        }
        else
        {
            weaponHold = null;
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
        if (weaponHold != null)
        {
            
            weaponHold.gameObject.SetActive(true);
        }
    }
    public void UnEquipItem()
    {
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f);
        if (weaponHold != null)
        {           
            weaponHold.gameObject.SetActive(false);
        }
        
    }

}
