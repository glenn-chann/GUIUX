using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    GUIController controller;

    private void Start()
    {
        controller = GameObject.Find("Player").GetComponent<GUIController>();
    }
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        Debug.Log(item.name);
        switch (item.itemType)
        {
            case Item.ItemType.Consumable:
                controller.foodBar.fillAmount += item.value/10;
                RemoveItem();
                break;
            case Item.ItemType.NonConsumable:
                break;
        }
    }
}
