using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveToPlayer : MonoBehaviour
{
        [SerializeField] InventoryItemData referenceData;

    public void Give()
    {

        InventorySystem.current.Add(referenceData);
    }
        
        
}
