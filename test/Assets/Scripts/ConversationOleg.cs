using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class ConversationOleg : MonoBehaviour
{
        [SerializeField] private NPCConversation myConversation;
        private int InteractionNumber = Suzhet.Oleg;


    
    void OnMouseDown(){
        if (!ConversationStarter.IsInv && !PlayerController.IsTalking && !PlayerController.IsUsing && !PlayerController.IsSearching && !UseOnItem.IsUse)
        {
            PlayerController.IsTalking = true;
            Debug.Log("Talk: ");
            ConversationManager.Instance.StartConversation(myConversation);
            ConversationManager.Instance.SetInt("InteractionNumber", InteractionNumber++);
            Suzhet.Oleg = InteractionNumber;
        }
        
}
   
}
