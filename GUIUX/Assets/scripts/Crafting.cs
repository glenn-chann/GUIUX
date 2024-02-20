using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class Crafting : MonoBehaviour
{
    public InventoryManager inventoryManager;
    int NoOfStone;
    int NoOfStick;
    List<Item> itemsToRemove = new List<Item>();
    public Image PickaxeRed;

    bool isCraftable = false;

    [HideInInspector] public bool pickaxeCrafted = false;
    public void PickaxeButton()
    {
        Debug.Log("Pressed");
        if (CheckPickaxe())
        {
            foreach (var item in itemsToRemove)
            {
                inventoryManager.Remove(item);
            }
            isCraftable = false;
            InventoryManager.Instance.DestroyItems();
            inventoryManager.ListItems();
            UpdateColor();
            pickaxeCrafted = true;
        }
    }

    public bool CheckPickaxe()
    {
        if (isCraftable)
        {
            return true;
        }
        foreach (var item in inventoryManager.Items)
        {
            if (item.id == 1 && NoOfStone < 2)
            {
                NoOfStone++;
                itemsToRemove.Add(item);
            }
            if (item.id == 2)
            {
                NoOfStick++;
                itemsToRemove.Add(item);
            }
        }
        if (NoOfStone >= 2 && NoOfStick >= 1)
        {
            Debug.Log("Enough");
            isCraftable = true;
            NoOfStone = 0;
            NoOfStick = 0;
            return true;
        }
        Debug.Log("Not Enough");
        itemsToRemove.Clear();
        NoOfStone = 0;
        NoOfStick = 0;
        return false;
    }

    public void UpdateColor()
    {
        if (CheckPickaxe())
        {
            Debug.Log("Nothing");
            Color c = PickaxeRed.color;
            c.a = 0;
            PickaxeRed.color = c;
        }
        else
        {
            Color c = PickaxeRed.color;
            c.a = 0.17f;
            PickaxeRed.color = c;
            Debug.Log("Red");
        }
    }
}
