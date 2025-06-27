using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject rulesPanel;
    public GameObject gamePanel;
    public CardGameController cardGameController;

    void Start()
    {
        PlayerController.IsSearching = true;
        rulesPanel.SetActive(true);
        gamePanel.SetActive(false);
        
    }

    public void StartGame()
    {
        PlayerController.IsSearching = true;
        rulesPanel.SetActive(false);
        gamePanel.SetActive(true);
        cardGameController.StartNewGame();
        
    }

    public void ExitGame()
    {
        rulesPanel.SetActive(true);
        gamePanel.SetActive(false);
        
    }

    public void HideRules()
    {
        rulesPanel.SetActive(false);
        PlayerController.IsSearching = false;
    }
}
