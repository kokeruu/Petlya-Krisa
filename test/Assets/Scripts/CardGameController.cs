using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameController : MonoBehaviour
{
    public CardCollection cardCollection;
    public CardUIController cardUI;
    public Text pointsText;
    public Text resultText;

    private List<CardData> deck;
    private List<CardData> playerHand;
    private bool isGameOver = false;

    public void StartNewGame()
    {
        deck = new List<CardData>(cardCollection.cards);
        ShuffleDeck();
        playerHand = new List<CardData>();
        isGameOver = false;
        resultText.text = "";

        DrawCard();
        DrawCard();
        UpdateUI();
    }

    public void DrawCard()
    {
        if (isGameOver || deck.Count == 0)
            return;

        int index = Random.Range(0, deck.Count);
        CardData drawnCard = deck[index];
        deck.RemoveAt(index);

        playerHand.Add(drawnCard);
        UpdateUI();

        if (CalculatePoints() > 21)
        {
            GameOver("Ты проиграл! Перебор.");
        }
    }

    public void Stand()
    {
        int playerTotal = CalculatePoints(playerHand);
        Debug.Log($"Игрок остановился. Очки: {playerTotal}");

        // Имитируем ход дилера: он берёт карты пока не наберёт 17 и больше
        List<CardData> dealerHand = new List<CardData>();
        int dealerTotal = 0;

        while (dealerTotal < 17)
        {
            if (deck.Count == 0) break;
            int index = Random.Range(0, deck.Count);
            CardData drawnCard = deck[index];
            deck.RemoveAt(index);
            dealerHand.Add(drawnCard);
            dealerTotal = CalculatePoints(dealerHand);
        }

        Debug.Log($"Дилер набрал: {dealerTotal}");

        string result;

        if (dealerTotal > 21 || playerTotal > dealerTotal)
            result = "Ты победил!";
        else if (dealerTotal == playerTotal)
            result = "Ничья!";
        else
            result = "Ты проиграл.";

        // Покажи результат в UI (если есть)
        if (resultText != null)
            resultText.text = result;
    }


    private void GameOver(string message)
    {
        isGameOver = true;
        resultText.text = message;
    }

    private void UpdateUI()
    {
        cardUI.ShowCards(playerHand);
        pointsText.text = "Очки: " + CalculatePoints();
    }

    private int CalculatePoints()
    {
        int total = 0;
        foreach (var card in playerHand)
        {
            total += card.cardValue;
        }
        return total;
    }

    private void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int j = Random.Range(0, deck.Count);
            var temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }
}
