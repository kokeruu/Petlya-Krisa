using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class ConversationHoll: MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    public static bool IsInv = false;
    public int InteractionNumber = Suzhet.Oleg;

    void OnMouseDown()
    {
        if (!ConversationStarter.IsInv && !PlayerController.IsTalking && !PlayerController.IsUsing && !PlayerController.IsSearching && !UseOnItem.IsUse && Suzhet.Holl)
        {
            PlayerController.IsTalking = true;
            Debug.Log("Talk: ");
            ConversationManager.Instance.StartConversation(myConversation);
            Suzhet.Holl = false;
        }
    }
    
}