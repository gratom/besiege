using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CityManager", menuName = "Scriptables/city", order = 51)]
public class CityManager : ScriptableObject
{
    public int orderMaxSum;
    public CityName currentCity;

    public bool canGround;
    public bool canWater;
    public bool canAir;

    public List<City> cities;
}

[Serializable]
public class City
{
    public CityName name;
    public bool isGrounded;
    public bool isWater;
    public bool isAir;
    public List<Product> ProductsToSell;
    public List<Product> ProductsTo;
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
    public int price;
}

public enum ProductType
{
    barrel,
    hay,
    wood,
    steel,
    ironOre,
    coal,
    fabric,
    gold
}
