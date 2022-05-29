using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[System.Serializable]

public class House
{
    public string Name;
    public Color32 ColorHouse;
    public int goldAdd;
}

[System.Serializable]

public class HouseList
{
    public List<House> houses = new List<House>();
}
[System.Serializable]

public class Monster
{
    public string Name;
    public Sprite MonsterImg;
    public InventoryData MonserItem;
}
[System.Serializable]

public class MonsterList
{
    public List<Monster> monsters = new List<Monster>();
}
[System.Serializable]
public class Order {
    public string NameOrder;
    public int _houseNumber;
    public int _monsterNumber;
    public int Gold;
    public int EnemyCount;
}
[CreateAssetMenu(menuName = "OrderBase/OrderDataBase")]

public class OrderDataBase : ScriptableObject
{
    [Header("House And Monster Data Base")]
    [Space(10)]

    public HouseList house;
    public MonsterList monsters;
    [Space(10)]

    [Header("Order Data Base")]
    [Space(10)]


    public List<Order> _Order ;
    // Start is called before the first frame update

    public void AddOrder(int NumberAdd)
    {

      //  List<int> SetOrd;
        var rH = Random.Range(0, house.houses.Count);
        var rM = Random.Range(0, monsters.monsters.Count);
        if (_Order.Count == 0)
        {
            _Order.Add(new Order { _houseNumber = rH, _monsterNumber = rM, NameOrder = house.houses[rH].Name + " -> " + monsters.monsters[rM].Name ,EnemyCount = 1,Gold = Random.Range(10, 30) }) ;

        }
        else
        {


         for (int i = 0; i < NumberAdd; i++)
         {
                if (_Order.Count + 1 <= house.houses.Count&& _Order.Count+1<= monsters.monsters.Count)
                {
                    do
                    {
                        rH = Random.Range(0, house.houses.Count);
                    } while (_Order.Any(x => x._houseNumber == rH));
                    do
                    {
                        rM = Random.Range(0, monsters.monsters.Count);
                    } while (_Order.Any(x => x._monsterNumber == rM));
                    _Order.Add(new Order { _houseNumber = rH, _monsterNumber = rM, NameOrder = house.houses[rH].Name +" -> "+ monsters.monsters[rM].Name, EnemyCount = 1, Gold = Random.Range(10, 30) });
                   

                }
            }
        }
    }

    public void RemoveOrder(int NumberOrder)
    {
        _Order.RemoveAt(NumberOrder);
    }
    
}
