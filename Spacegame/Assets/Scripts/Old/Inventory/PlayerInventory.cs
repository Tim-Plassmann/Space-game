using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public class Item
    {
        public string itemName;
        public int itemID;
        // Add more properties as needed

        public Item(string name, int id)
        {
            itemName = name;
            itemID = id;
        }
    }
    public int inventorySize = 10; // Set the size of the inventory
    public List<Item> items = new List<Item>(); // List to store items

    // Add an item to the inventory
    public void AddItem(Item newItem)
    {
        // Check if the inventory is full
        if (items.Count < inventorySize)
        {
            items.Add(newItem);
            Debug.Log("Added " + newItem.itemName + " to the inventory.");
        }
        else
        {
            Debug.Log("Inventory is full. Cannot add " + newItem.itemName + ".");
        }
    }

    // Remove an item from the inventory
    public void RemoveItem(Item itemToRemove)
    {
        if (items.Contains(itemToRemove))
        {
            items.Remove(itemToRemove);
            Debug.Log("Removed " + itemToRemove.itemName + " from the inventory.");
        }
        else
        {
            Debug.Log("Item not found in the inventory.");
        }
    }

    // Use an item from the inventory (you can modify this based on your item usage logic)
    public void UseItem(Item itemToUse)
    {
        // Implement item usage logic here
        Debug.Log("Used " + itemToUse.itemName + ".");
    }

}
