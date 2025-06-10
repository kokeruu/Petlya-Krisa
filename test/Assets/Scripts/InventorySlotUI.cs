using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    // [SerializeField] private Text countText;
    // [SerializeField] private Button slotButton;
    
    private InventoryItem item;
    private System.Action<InventorySlotUI> onClick;

    public InventoryItem Item => item;
    private void Awake()
    {
        // Автоматическое получение ссылок если не установлены
        // if (slotButton == null) slotButton = GetComponent<Button>();
        if (icon == null) icon = GetComponentInChildren<Image>();
        
        // Назначаем обработчик клика
        // slotButton.onClick.AddListener(OnClick);
    }
    public void Initialize(InventoryItem newItem, System.Action<InventorySlotUI> clickAction)
    {
        item = newItem;
        onClick = clickAction;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (item != null)
        {
            icon.sprite = item.data.icon;
            icon.enabled = true;
            // countText.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
        }
        else
        {
            icon.enabled = false;
            // countText.text = "";
        }
    }

    // Вызывается при клике на слот (назначьте в инспекторе)
    public void OnClick()
    {
        onClick?.Invoke(this);
    }
}