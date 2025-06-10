using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGameController : MonoBehaviour
{
    public CardCollection cardCollection;

    private List<CardData> deck;
    private List<CardData> playerHand;

    void Start()
    {
        StartNewGame();
    }

    public void StartNewGame()
    {
        deck = new List<CardData>(cardCollection.cards); // копируем карты
        ShuffleDeck();
        playerHand = new List<CardData>();

        DrawCard();
        DrawCard();
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

        Debug.Log($"Выдана карта: {drawnCard.cardName} ({drawnCard.cardValue})");
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
