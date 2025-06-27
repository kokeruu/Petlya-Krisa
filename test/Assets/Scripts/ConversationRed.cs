using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class ConversationRed : MonoBehaviour
{
        [SerializeField] private NPCConversation myConversation;
        public static bool IsInv = false;
        private int InteractionNumber = Suzhet.Red;


    
    void OnMouseDown(){
        if (!ConversationStarter.IsInv && !PlayerController.IsTalking && !PlayerController.IsUsing && !PlayerController.IsSearching && !UseOnItem.IsUse)
        {
            PlayerController.IsTalking = true;
            Debug.Log("Talk: ");
            ConversationManager.Instance.StartConversation(myConversation);
            ConversationManager.Instance.SetInt("InteractionNumber", InteractionNumber++);
            Suzhet.Red = InteractionNumber;
        }
        
}
   
}
