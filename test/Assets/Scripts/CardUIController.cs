using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardUIController : MonoBehaviour
{
    public RectTransform cardHolder;  // Контейнер для карт (изменили на RectTransform)
    public GameObject cardPrefab; // Префаб карты с Image

    public float cardSpacing = 20f; // Расстояние между картами

    private List<GameObject> activeCards = new List<GameObject>(); // Список текущих карт

    public void ShowCard(Sprite sprite)
    {
        GameObject card = Instantiate(cardPrefab, cardHolder);
        card.GetComponent<Image>().sprite = sprite;

        activeCards.Add(card);

        UpdateCardsPosition();
    }

    public void ClearCards()
    {
        foreach (Transform child in cardHolder)
        {
            Destroy(child.gameObject);
        }
        activeCards.Clear();
    }

    private void UpdateCardsPosition()
    {
        int cardCount = activeCards.Count;
        if (cardCount == 0) return;

        // Получаем ширину одной карты
        float cardWidth = activeCards[0].GetComponent<RectTransform>().rect.width;
    
        // Общая ширина всех карт + промежутки
        float totalWidth = (cardWidth * cardCount) + (cardSpacing * (cardCount - 1));
    
        // Начальная позиция (первая карта будет на startX, остальные справа от неё)
        float startX = -totalWidth / 2f + cardWidth / 2f;

        // Убедимся, что контейнер обновляет свою геометрию
        LayoutRebuilder.ForceRebuildLayoutImmediate(cardHolder);

        for (int i = 0; i < cardCount; i++)
        {
            RectTransform cardRT = activeCards[i].GetComponent<RectTransform>();
        
            // Явно задаём anchors и pivot в центр (если они не установлены в префабе)
            cardRT.anchorMin = new Vector2(0.5f, 0.5f);
            cardRT.anchorMax = new Vector2(0.5f, 0.5f);
            cardRT.pivot = new Vector2(0.5f, 0.5f);
        
            // Позиционируем карту
            float xPos = startX + i * (cardWidth + cardSpacing);
            cardRT.anchoredPosition = new Vector2(xPos, 0f);
        
            // Логируем позиции для отладки
            Debug.Log($"Card {i}: xPos = {xPos}");
        }
    }
}