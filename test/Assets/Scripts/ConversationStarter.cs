using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    public static bool IsInv = false;
    public int InteractionNumber = Suzhet.Oleg;

    // Публичный метод для запуска диалога из других скриптов
    public void StartConversationExternally()
    {
        if (!IsInv && !PlayerController.IsTalking && !PlayerController.IsUsing && 
            !PlayerController.IsSearching && !UseOnItem.IsUse)
        {
            PlayerController.IsTalking = true;
            Debug.Log("Talk started externally");
            ConversationManager.Instance.StartConversation(myConversation);
            ConversationManager.Instance.SetInt("InteractionNumber", InteractionNumber++);
        }
    }

    void OnMouseDown()
    {
        // Теперь просто вызывает общий метод
        StartConversationExternally();
    }
}