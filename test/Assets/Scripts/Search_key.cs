using System.Collections;
using System.Collections.Generic;
using System.IO;
using DialogueEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class Search_key : MonoBehaviour
{
        [SerializeField] private NPCConversation SearchIn;
        public static bool IsUse = false;
        [SerializeField] private NPCConversation useOn;
        [SerializeField] private bool IsUsable;
    public void OnMouseDown()
    {
        PlayerController.IsMoving = false;
        PlayerController.anim.SetBool("IsMoving", false);
        if (!ConversationStarter.IsInv && !PlayerController.IsTalking)
        {
            if (!UseOnItem.IsUse)
            {
                PlayerController.IsSearching = true;
                Debug.Log("Searching: ");
                ConversationManager.Instance.StartConversation(SearchIn);
                ConversationManager.Instance.SetBool("IsFull", true);
                Suzhet.IsKeyTaken = true;
            }
            if (UseOnItem.IsUse)
            {
                PlayerController.IsUsing = true;
                ConversationManager.Instance.SetBool("IsUsable", IsUsable);
                Debug.Log("Using: ");
                ConversationManager.Instance.StartConversation(useOn);
                
            }
        }


    }
   
}
