using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ExamineWindow : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        closeButton.onClick.AddListener(CloseWindow);
        window.SetActive(false);
    }

    public void ShowItem(InventoryItemData itemData)
    {
        itemNameText.text = itemData.displayName;
        descriptionText.text = itemData.examineDescription;
        itemImage.sprite = itemData.icon;
        
        window.SetActive(true);
    }

    public void CloseWindow()
    {
        window.SetActive(false);
    }
}