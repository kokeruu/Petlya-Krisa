using System.Collections;
using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;
using UnityEngine.EventSystems;
public class Karti_Vasa : MonoBehaviour
{
        [SerializeField] private NPCConversation myConversation;
        public static bool IsInv = false;


    
    void OnMouseDown(){
        if (!PlayerController.IsTalking && !PlayerController.IsUsing && !PlayerController.IsSearching && UseOnItem.IsUse)
        {
            PlayerController.IsTalking = true;
            Debug.Log("Talk: ");
            ConversationManager.Instance.StartConversation(myConversation);
            ConversationManager.Instance.SetBool("ifyouwin",  Suzhet.ifyouwin);
            
        }
        
}
   
}
