﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "VirusDestroyer/ShopItem", order = 1500)]
public class ShopItemBase : ScriptableObject
{
    public InventoryItems itemName;
    public Sprite sprite;
    public int itemPrice;
    public string itemStringName;
}
