using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;
public class Empty : MonoBehaviour
{
public NPCConversation myConversation;


    public void OnMouseDown()
    {

        ConversationManager.Instance.StartConversation(myConversation);
    }
  
}
