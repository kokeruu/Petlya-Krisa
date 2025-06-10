using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{


    private void OnMouseDown()
    {
        PickupItem();
    }

    public void PickupItem()
    {
        // Проверяем, есть ли инвентарь в сцене
        if (InventorySystem.current != null)
        {
            Destroy(gameObject);  
            Debug.Log("добавила!");
        }
        else
        {
            Debug.LogError("InventorySystem not found in scene!");
        }
    }
}