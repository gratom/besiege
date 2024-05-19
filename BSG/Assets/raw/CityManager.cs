using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CityManager", menuName = "Scriptables/city", order = 51)]
public class CityManager : ScriptableObject
{
    public int orderMaxSum;
    public CityName currentCity;

    public List<Order> orders;

    public bool canGround;
    public bool canWater;
    public bool canAir;

    public List<City> cities;

    [ContextMenu("New orders")]
    public void GetNewOrders()
    {
    }

}

[Serializable]
public class Order
{
    [Tooltip("Купить контракт")]
    public int buySum;
    [Tooltip("Получить денег после выполнения")]
    public int sellSum;

    public List<Product> products;
}

[Serializable]
public class City
{
    public CityName name;
    public bool isGrounded;
    public bool isWater;
    public bool isAir;
    public List<Product> ProductsToSell;
    public List<Product> ProductsToBuy;
}

public enum CityName
{
    pervorym,
    factoria
}

[Serializable]
public class Product
{
    public ProductType type;
    public int count;
    public int price;
}

public enum ProductType
{
    barrel,
    hay,
    wood,
    bigWood,
    ironOre,
    bigIronOre,
    steel,
    coal,
    fabric,
    gold,
    bomb
}
