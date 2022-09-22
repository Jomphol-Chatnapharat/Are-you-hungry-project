using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
[System.Serializable]
public class UIOrder
{
    public Image HouseColor;
    public Text MonsterText;
    public Text CountText;
}
public class OrderManager : MonoBehaviour
{
    public OrderDataBase DataBase;
    public int OrderSet;
    public bool OrderTake;
    int countNow;

    public UIOrder[] uIOrders;
    public UIOrder TakeOrder;
    public GameObject OrderCanvas;
    public GameObject CanvasInv;
    public PostProcessVolume processVolume;
    public PlayerBehavior player;
    [SerializeField] private PathFind pathcalculator;
    
    // Start is called before the first frame update
    void Start()
    {
        DataBase._Order.Clear();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OrderCanvas.SetActive(true);
            //CanvasInv.SetActive(false);
            UpdateUI();
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            processVolume.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            OrderCanvas.SetActive(false);
           // CanvasInv.SetActive(true) ;
            UpdateUI();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            processVolume.enabled = false;
        }

        if (DataBase._Order.Count != 3)
        {
            DataBase.AddOrder(3);
            DataBase.AddOrder(3);
        }
    }

    public void AddAndCheckOrder(InventoryData itm)
    {

         if (OrderTake && DataBase._Order[OrderSet].EnemyCount != countNow )
        {
            countNow++;
        }
         if (OrderTake && DataBase._Order[OrderSet].EnemyCount == countNow )
        {

            Debug.Log("COMPLETE ORDER");
            CompleteOrder();
        
        }
        UpdateUI();

    }
   public void RemoveCount(InventoryData itm)
    {
        if(OrderTake && DataBase.monsters.monsters[DataBase._Order[OrderSet]._monsterNumber].MonserItem.id == itm.id)
        {
            Debug.Log("Remove1");
            countNow--;
            UpdateUI();
        }
    }
    public void CompleteOrder()
    {
        OrderTake = false;
        pathcalculator.CalculatePath(OrderSet);
        countNow = 0;
        player.currentGold += DataBase._Order[OrderSet].Gold;
        DataBase._Order.RemoveAt(OrderSet);
        DataBase.AddOrder(1);
        OrderSet = 0;
        UpdateUI();
    }
    public void SetOrder(int i)
    {
        if (OrderTake == false)
        {
            OrderSet = i;
            OrderTake = true;
            UpdateUI();            
        }
        

    }
    public void DeliveryDroneCheck(InventoryData itm)
    {
        for (int i = 0; i <= 8; i++) // I've just used max house id, you should change it so it will be changeable with number of houses
        {
            if (ItemTrue(itm, i))
            {
                AddAndCheckOrder(itm);
                TakeOrder.CountText.text = "+";
            }
        }
    }
    public void UpdateUI()
    {
        if(OrderTake == false)
        {
            for(int i = 0 ;i < DataBase._Order.Count;i++)
            {
                uIOrders[i].HouseColor.color = DataBase.house.houses[DataBase._Order[i]._houseNumber].ColorHouse;
                uIOrders[i].MonsterText.text = DataBase.monsters.monsters[DataBase._Order[i]._monsterNumber].Name;
                TakeOrder.HouseColor.gameObject.SetActive(false);

            }
        }
        if (OrderTake)
        {
            TakeOrder.HouseColor.gameObject.SetActive(true);
            TakeOrder.HouseColor.color = DataBase.house.houses[DataBase._Order[OrderSet]._houseNumber].ColorHouse;
            TakeOrder.MonsterText.text = DataBase.monsters.monsters[DataBase._Order[OrderSet]._monsterNumber].Name.ToString();
            if (OrderTake && DataBase._Order[OrderSet].EnemyCount != countNow)
            {
                TakeOrder.CountText.text = countNow.ToString() + " / " + DataBase._Order[OrderSet].EnemyCount.ToString();
            }
            if (OrderTake && DataBase._Order[OrderSet].EnemyCount == countNow)
            {
                TakeOrder.CountText.text = "+";
            }
           
        }
    }
    public bool ItemTrue(InventoryData itm,int houseId)
    {

        if (DataBase.monsters.monsters[DataBase._Order[OrderSet]._monsterNumber].MonserItem.id == itm.id && DataBase._Order[OrderSet]._houseNumber == houseId)
        {
            return true;
        }else return false;

        

    }
}
