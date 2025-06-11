using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class UseOnItem : MonoBehaviour
{
    [SerializeField] private NPCConversation useOn;
    public static bool IsUse = false;
    [SerializeField] private bool IsUsable;
    void OnMouseDown()
    {
        if (IsUse)
        {
            ConversationManager.Instance.SetBool("IsUsable", IsUsable);
            Debug.Log("Using: ");
            ConversationManager.Instance.StartConversation(useOn);
            PlayerController.IsTalking = true;
            PlayerController.IsUsing= true;
            Debug.Log("Talk: ");
        }
    }
}
