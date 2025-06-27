using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleLock : MonoBehaviour
{
    public TMP_InputField[] inputFields;
    public GameObject panel;
    public Button checkButton;
    public Button closeButton;            
    public TextMeshProUGUI feedbackText;  
    public GameObject notebookButton;
    public ConversationStarter conversationStarter;

    private string correctCode = "3854";

    void Start()
    {
        PlayerController.IsSearching = true;
        checkButton.onClick.AddListener(CheckCode);
        closeButton.onClick.AddListener(ClosePanel);

        feedbackText.gameObject.SetActive(false);
        panel.SetActive(false);

        if (PlayerPrefs.GetInt("NotebookUnlocked", 0) == 1)
        {
            notebookButton.SetActive(true);
        }
        else
        {
            notebookButton.SetActive(false);
        }
    }

    public void OpenPanel()
    {
        PlayerController.IsTalking = true;
        panel.SetActive(true);
        ClearInputs();
        
    }

    void CheckCode()
    {
        string entered = "";
        foreach (var field in inputFields)
        {
            entered += field.text;
        }

        feedbackText.gameObject.SetActive(true);

        if (entered == correctCode)
        {
            feedbackText.text = "Правильно!";
            // Можешь отключить ввод, или вызвать финальное событие
            PlayerPrefs.SetInt("NotebookUnlocked", 1);
            notebookButton.SetActive(true);
            Invoke("ClosePanel", 1.5f);  // Автоматически закроется через 1.5 секунды
            PlayerController.IsSearching = false;
            GameObject npc = GameObject.FindGameObjectWithTag("NPC");
            if (npc != null && npc.TryGetComponent(out ConversationStarter starter))
            {
                starter.StartConversationExternally();
            }
            

        }
        else
        {
            feedbackText.text = "Неверный код";
        }
    }

    void ClosePanel()
    {
        panel.SetActive(false);
        PlayerController.IsTalking = false;

    }

    void ClearInputs()
    {
        foreach (var field in inputFields)
        {
            field.text = "";
        }

        feedbackText.gameObject.SetActive(false);
    }
}
