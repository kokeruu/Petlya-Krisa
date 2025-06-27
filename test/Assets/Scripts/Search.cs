using UnityEngine;
using UnityEngine.EventSystems;
using DialogueEditor;
using System.Collections;

public class Search : MonoBehaviour
{
    [SerializeField] private NPCConversation SearchIn;
    [SerializeField] private bool IsFull;
    public static bool IsUse = false;
    [SerializeField] private NPCConversation useOn;
    [SerializeField] private bool IsUsable;

   public void OnMouseDown()
{
    // Проверка на клик по UI
    if (EventSystem.current.IsPointerOverGameObject()) return;
    
    // Проверка доступности диалога
    if (PlayerController.IsTalking || ConversationStarter.IsInv) 
        return;

    // Защита от null
    if (ConversationManager.Instance == null)
    {
        Debug.LogError("ConversationManager не инициализирован!");
        return;
    }

    PlayerController.IsMoving = false;
    PlayerController.anim.SetBool("IsMoving", false);

    // Отложенный старт диалога
    StartCoroutine(StartDialogueWithCheck());
}

private IEnumerator StartDialogueWithCheck()
{
    // Ждем инициализации ConversationManager
    while (ConversationManager.Instance == null)
    {
        yield return null;
    }

    if (!UseOnItem.IsUse)
    {
        StartSearchDialogue();
    }
    if (UseOnItem.IsUse)
    {
        StartUseDialogue();
    }
}

    private void StartSearchDialogue()
    {
        if (SearchIn == null)
        {
            Debug.LogError("SearchIn conversation is not assigned!");
            return;
        }

        PlayerController.IsSearching = true;
        ConversationManager.Instance.StartConversation(SearchIn);
        ConversationManager.Instance.SetBool("IsFull", IsFull);
        IsFull = false;
    }

    private void StartUseDialogue()
    {
        if (useOn == null)
        {
            Debug.LogError("useOn conversation is not assigned!");
            return;
        }

        PlayerController.IsUsing = true;

        ConversationManager.Instance.StartConversation(useOn);
        ConversationManager.Instance.SetBool("IsUsable", IsUsable);
        ConversationManager.Instance.SetInt("Effect", UseOnItem.Effect);
    }
}