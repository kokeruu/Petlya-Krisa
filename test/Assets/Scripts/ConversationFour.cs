using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class ConversationFour: MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    public static bool IsInv = false;

    void OnMouseDown()
    {
        if (!ConversationStarter.IsInv && !PlayerController.IsTalking && !PlayerController.IsUsing && !PlayerController.IsSearching && !UseOnItem.IsUse && !Suzhet.Is4)
        {
            PlayerController.IsTalking = true;
            Debug.Log("Talk: ");
            ConversationManager.Instance.StartConversation(myConversation);

        }
    }
    
}