using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class invTest : MonoBehaviour
{
    public PlayerBehavior player;
    public InventoryData[] Inventory;
    public OrderManager _OrdManager;
    public int count;
    public bool invFull = false;
    public float maxWeight;
    public float currentWeight;
    public WeaponItem weaponOnInv;
    public void Start()
    {
    }
    public void AddToInv(InventoryData itemData)
    {
        currentWeight = 0;


        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] != null)
            {
                currentWeight += Inventory[i].Weight;
            }
            if (Inventory[i] == null && (currentWeight+itemData.Weight)<=maxWeight )
            {
                Inventory[i] = itemData;
                currentWeight += itemData.Weight;
                Debug.Log(itemData.id.ToString());
              //  _OrdManager.AddAndCheckOrder(itemData);

                count += 1;

                break;
            }
        }
    }
    public void UpdateBar()
    {
        //currentWeight = 0;
       player.WeightBar.SetActive(true);
        player.textsBar.text = currentWeight.ToString() + "/" + maxWeight.ToString();
        float x = currentWeight / maxWeight;
        player.BarFilled.fillAmount = x;
       
        }
    public void RotateBar()
    {
       // WeightBar.transform.LookAt(2 * WeightBar.transform.position - Camera.main.transform.position);

        //transform.LookAt(2 * transform.position - lookingAt.position);
    }
    public bool CheckWeight(InventoryData itemdData)
    {
        if((currentWeight + itemdData.Weight) <= maxWeight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckInv()
    {
        //for (int j = 0; j < Inventory.Length; j++)
        //{
        //    if (Inventory[j] != null)
        //    {
        //        count += 1;
        //    }
        //}

        if (count >= Inventory.Length)
        {
            invFull = true;
        }
        else
        {
            invFull = false;
        }
    }

    public void PickEnemy0()
    {
            if (player.grabbedRB == null && Inventory[0] != null)
            {
                Debug.Log("condition pass");

                    Vector3 holderPos = new Vector3(player.objectHolder.position.x, player.objectHolder.position.y, player.objectHolder.position.z);

                    
                    Instantiate(Inventory[0].prefab, holderPos, Quaternion.identity);
          //  _OrdManager.RemoveCount(Inventory[0]);
            Inventory[0] = null;
           
                count -= 1;

            //player.HoldingObj();
            }
    }

    public void PickEnemy1()
    {
        if (player.grabbedRB == null && Inventory[1] != null)
        {
            Debug.Log("condition pass");

            Vector3 holderPos = new Vector3(player.objectHolder.position.x, player.objectHolder.position.y, player.objectHolder.position.z);


            Instantiate(Inventory[1].prefab, holderPos, Quaternion.identity);
          //  _OrdManager.RemoveCount(Inventory[1]);
            Inventory[1] = null;

            count -= 1;

            //player.HoldingObj();
        }
    }

    public void PickEnemy2()
    {
        if (player.grabbedRB == null && Inventory[2] != null)
        {
            Debug.Log("condition pass");

            Vector3 holderPos = new Vector3(player.objectHolder.position.x, player.objectHolder.position.y, player.objectHolder.position.z);


            Instantiate(Inventory[2].prefab, holderPos, Quaternion.identity);
           // _OrdManager.RemoveCount(Inventory[2]);
            Inventory[2] = null;

            count -= 1;

            //player.HoldingObj();
        }
    }

    public void PickEnemy3()
    {
        if (player.grabbedRB == null && Inventory[3] != null)
        {
            Debug.Log("condition pass");

            Vector3 holderPos = new Vector3(player.objectHolder.position.x, player.objectHolder.position.y, player.objectHolder.position.z);


            Instantiate(Inventory[3].prefab, holderPos, Quaternion.identity);
          //  _OrdManager.RemoveCount(Inventory[3]);
            Inventory[3] = null;

            count -= 1;

            //player.HoldingObj();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.GetComponent<SimpleEnemy>() != null)
        {
            if (currentWeight != 0)
            {
                collision.gameObject.GetComponent<SimpleEnemy>().OnDamaged((int)currentWeight);
            }
            else
            {
                collision.gameObject.GetComponent<SimpleEnemy>().OnDamaged(5);
            }
        }
        
    }
}
