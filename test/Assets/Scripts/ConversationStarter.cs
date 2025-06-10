using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class ConversationStarter : MonoBehaviour
{
        [SerializeField] private NPCConversation myConversation;
        public static bool IsInv = false;


    
    void OnMouseDown(){
        if (!IsInv)
        {
            Debug.Log("Conversation: ");
            ConversationManager.Instance.StartConversation(myConversation);
            PlayerController.IsTalking = true;
            Debug.Log("Talk: ");
        }
        
}
   
}
