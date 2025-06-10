using UnityEngine;
using UnityEngine.UI;

public class CardUIController : MonoBehaviour
{
    public Transform cardHolder;
    public GameObject cardPrefab;

    public void ShowCard(Sprite sprite)
    {
        GameObject card = Instantiate(cardPrefab, cardHolder);
        card.GetComponent<Image>().sprite = sprite;
    }

    public void ClearCards()
    {
        foreach (Transform child in cardHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
