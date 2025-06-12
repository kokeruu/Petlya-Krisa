using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class Search : MonoBehaviour
{
        [SerializeField] private NPCConversation SearchIn;
        [SerializeField] private bool IsFull;
        [SerializeField] InventoryItemData referenceData;

    public void OnMouseDown()
    {
        if (IsFull)
        {
            InventorySystem.current.Add(referenceData);
        }
        if (!ConversationStarter.IsInv && !UseOnItem.IsUse)
            {
                Debug.Log("Searching: ");
                ConversationManager.Instance.StartConversation(SearchIn);
                ConversationManager.Instance.SetBool("IsFull", IsFull);
                // PlayerController.IsSearching = true;

                IsFull = false;

            }
        
        
}
   
}
