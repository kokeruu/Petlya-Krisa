using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    public GameObject rulesPanel;
    public GameObject gamePanel;
    public CardGameController cardGameController;

    void Start()
    {
        rulesPanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void StartGame()
    {
        rulesPanel.SetActive(false);
        gamePanel.SetActive(true);
        cardGameController.StartNewGame();
    }

    public void ExitGame()
    {
        rulesPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
}
