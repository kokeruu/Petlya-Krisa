using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardUIController : MonoBehaviour
{
    public Transform cardHolder;
    public GameObject cardPrefab;

    private List<GameObject> spawnedCards = new List<GameObject>();

    public void ShowCards(List<CardData> hand)
    {
        ClearCards();

        foreach (var card in hand)
        {
            GameObject cardGO = Instantiate(cardPrefab, cardHolder);
            cardGO.GetComponent<Image>().sprite = card.cardSprite;
            spawnedCards.Add(cardGO);
        }
    }

    public void ClearCards()
    {
        foreach (GameObject go in spawnedCards)
        {
            Destroy(go);
        }
        spawnedCards.Clear();
    }
}
