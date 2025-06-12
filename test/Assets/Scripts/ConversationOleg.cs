using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class ConversationOleg : MonoBehaviour
{
        [SerializeField] private NPCConversation myConversation;
        public static bool IsInv = false;
        private int InteractionNumber = Suzhet.Oleg;


    
    void OnMouseDown(){
        if (!IsInv && !PlayerController.IsTalking && !PlayerController.IsUsing && !PlayerController.IsSearching)
        {
            PlayerController.IsTalking = true;
            Debug.Log("Talk: ");
            ConversationManager.Instance.StartConversation(myConversation);
            ConversationManager.Instance.SetInt("InteractionNumber", InteractionNumber++);
            Suzhet.Oleg = InteractionNumber;
        }
        
}
   
}
