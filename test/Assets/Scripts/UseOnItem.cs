using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class UseOnItem : MonoBehaviour
{
    public static bool IsUse = false;
    public static int Effect = 0;
    [SerializeField] private NPCConversation useOn;
    [SerializeField] private bool IsUsable;
void OnMouseDown()
{
        if (!ConversationStarter.IsInv && !PlayerController.IsTalking && !PlayerController.IsUsing && !PlayerController.IsSearching && UseOnItem.IsUse)
        {
            PlayerController.IsUsing = true;
            ConversationManager.Instance.StartConversation(useOn);
            ConversationManager.Instance.SetBool("IsUsable", IsUsable);
            ConversationManager.Instance.SetInt("Effect", Effect);
            UseOnItem.IsUse = false;
    }
}
         
}

   