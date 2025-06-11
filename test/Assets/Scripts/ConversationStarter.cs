using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class ConversationStarter : MonoBehaviour
{
        [SerializeField] private NPCConversation myConversation;
        public static bool IsInv = false;
        public int InteractionNumber = 1;


    
    void OnMouseDown(){
        if (!IsInv && !PlayerController.IsTalking && !PlayerController.IsUsing && !PlayerController.IsSearching)
        {
            Debug.Log("Conversation: ");
            ConversationManager.Instance.StartConversation(myConversation);
            PlayerController.IsTalking = true;
            Debug.Log("Talk: ");
            ConversationManager.Instance.SetInt("InteractionNumber", InteractionNumber++);

            
        }
        
}
   
}
