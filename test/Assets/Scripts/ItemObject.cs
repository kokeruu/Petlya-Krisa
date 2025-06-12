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
        if (!PlayerController.IsTalking && !PlayerController.IsUsing)
        {if (InventorySystem.current != null)
        {
            Destroy(gameObject);  
            Debug.Log("добавила!");
        }
        else
        {
            Debug.LogError("InventorySystem not found in scene!");
        }}
    }
}