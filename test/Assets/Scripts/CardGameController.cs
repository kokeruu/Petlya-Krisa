using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;  // Для кнопок

public class CardGameController : MonoBehaviour
{
    public CardCollection cardCollection;
    public TMP_Text scoreText;
    public TMP_Text resultText;
    public CardUIController cardUIController;

    public Button hitButton;
    public Button standButton;
    public Button restartButton;

    private List<CardData> deck;
    private List<CardData> playerHand;

    private bool isGameOver = false;

    void Start()
    {
        SetupButtons();
        StartNewGame();
    }

    void SetupButtons()
    {
        restartButton.gameObject.SetActive(false);

        hitButton.onClick.RemoveAllListeners();
        standButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();

        hitButton.onClick.AddListener(Hit);
        standButton.onClick.AddListener(Stand);
        restartButton.onClick.AddListener(RestartGame);
    }

    public void StartNewGame()
    {
        isGameOver = false;
        resultText.text = "";
        deck = new List<CardData>(cardCollection.cards);
        ShuffleDeck();
        playerHand = new List<CardData>();
        cardUIController.ClearCards();

        DrawCard();
        DrawCard();
        UpdateScoreUI();

        hitButton.interactable = true;
        standButton.interactable = true;
        restartButton.gameObject.SetActive(false);
    }

    public void DrawCard()
    {
        if (deck.Count == 0)
        {
            Debug.Log("Колода пуста!");
            return;
        }

        int index = Random.Range(0, deck.Count);
        CardData drawnCard = deck[index];
        deck.RemoveAt(index);

        playerHand.Add(drawnCard);
        cardUIController.ShowCard(drawnCard.cardSprite);

        Debug.Log($"Выдана карта: {drawnCard.cardName} ({drawnCard.cardValue})");
    }

    public void Hit()
    {
        if (isGameOver) return;

        DrawCard();
        UpdateScoreUI();

        int score = CalculateScore();

        if (score > 21)
        {
            EndGame();
        }
    }

    public void Stand()
    {
        if (isGameOver) return;

        EndGame(playerStopped: true);
    }


    private void EndGame(bool playerStopped = false)
    {
        isGameOver = true;
        hitButton.interactable = false;
        standButton.interactable = false;
        restartButton.gameObject.SetActive(true);

        int playerScore = CalculateScore();
        int cardCount = playerHand.Count;
        int minOpponentScore = cardCount * 2;
        int maxOpponentScore = cardCount * 11;

        int opponentScore = Random.Range(minOpponentScore, maxOpponentScore + 1);

        if (playerScore > 21)
        {
            if (opponentScore > 21)
            {
                resultText.text = $"Ничья! У обоих перебор. У соперника: {opponentScore}";
            }
            else
            {
                resultText.text = $"Вы проиграли. У соперника: {opponentScore}";
            }
        }
        else if (playerStopped)
        {
            if (opponentScore > 21)
            {
                resultText.text = $"Вы победили! У соперника перебор: {opponentScore}";
            }
            else if (opponentScore > playerScore)
            {
                resultText.text = $"Вы проиграли. У соперника: {opponentScore}";
            }
            else if (opponentScore < playerScore)
            {
                resultText.text = $"Вы победили! У соперника: {opponentScore}";
            }
            else
            {
                resultText.text = $"Ничья! У соперника тоже {opponentScore}";
            }
        }
    }

    private void RestartGame()
    {
        StartNewGame();
    }

    private int CalculateScore()
    {
        int score = 0;
        int aceCount = 0;

        foreach (var card in playerHand)
        {
            score += card.cardValue;
            if (card.cardValue == 11) aceCount++;
        }

        while (score > 21 && aceCount > 0)
        {
            score -= 10;
            aceCount--;
        }

        return score;
    }

    private void UpdateScoreUI()
    {
        int score = CalculateScore();
        scoreText.text = "Счёт: " + score;
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int j = Random.Range(0, deck.Count);
            CardData temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }
}
