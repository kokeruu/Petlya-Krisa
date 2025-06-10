using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem current { get; private set; }

    private Dictionary<InventoryItemData, InventoryItem> itemDictionary;
    public List<InventoryItem> inventory { get; private set; }
    public event Action OnInventoryChanged;

    private const string SAVE_KEY = "InventoryData";

    private void Awake()
    {
        // Singleton pattern (destroy duplicates)
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }

        current = this;
        DontDestroyOnLoad(gameObject); // Persist between scenes

        inventory = new List<InventoryItem>();
        itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

        LoadInventory(); // Load saved data when initialized
    }

    public void Add(InventoryItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            itemDictionary.Add(referenceData, newItem);
        }
        OnInventoryChanged?.Invoke();
        SaveInventory(); // Save after modification
    }

    public void Remove(InventoryItemData referenceData)
    {
        if (itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();
            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                itemDictionary.Remove(referenceData);
            }
            OnInventoryChanged?.Invoke();
            SaveInventory(); // Save after modification
        }
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        itemDictionary.TryGetValue(referenceData, out InventoryItem value);
        return value;
    }

    // ===== Save & Load System =====
    private void SaveInventory()
    {
        List<InventorySaveData> saveData = new List<InventorySaveData>();
        foreach (var item in inventory)
        {
            saveData.Add(new InventorySaveData
            {
                itemId = item.data.id, // Ensure InventoryItemData has a unique ID
                stackSize = item.stackSize
            });
        }

        string json = JsonUtility.ToJson(new InventorySaveWrapper { items = saveData });
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    private void LoadInventory()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(SAVE_KEY);
            var wrapper = JsonUtility.FromJson<InventorySaveWrapper>(json);

            foreach (var savedItem in wrapper.items)
            {
                // You need a way to reference InventoryItemData by ID
                InventoryItemData itemData = GetItemDataById(savedItem.itemId);
                if (itemData != null)
                {
                    InventoryItem newItem = new InventoryItem(itemData);
                    newItem.stackSize = savedItem.stackSize; // Manually set stack size
                    inventory.Add(newItem);
                    itemDictionary.Add(itemData, newItem);
                }
            }
        }
    }

    // Helper method to find InventoryItemData by ID (you need to implement this)
    private InventoryItemData GetItemDataById(string id)
    {
        // Example: Load from Resources or use a ScriptableObject registry
        return Resources.Load<InventoryItemData>($"Items/{id}");
    }

    // ===== Debug & Testing =====
    private void LogInventory(string action)
    {
        Debug.Log($"{action} - Current inventory:");
        if (inventory.Count == 0) Debug.Log("Inventory is empty");
        else foreach (var item in inventory) Debug.Log($"- {item.data.displayName}: {item.stackSize}");
    }

    public void ClearSavedData()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        inventory.Clear();
        itemDictionary.Clear();
        OnInventoryChanged?.Invoke();
    }
}

// Helper classes for serialization
[Serializable]
public class InventorySaveWrapper
{
    public List<InventorySaveData> items;
}

[Serializable]
public class InventorySaveData
{
    public string itemId;
    public int stackSize;
}

[Serializable]
public class InventoryItem
{
    public InventoryItemData data;
    public int stackSize;

    public InventoryItem(InventoryItemData source)
    {
        data = source;
        stackSize = 1; // Start with 1 when created
    }

    public void AddToStack() => stackSize++;
    public void RemoveFromStack() => stackSize--;
}