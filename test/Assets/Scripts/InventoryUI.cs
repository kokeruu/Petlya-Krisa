using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Main Components")]
     [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Transform slotsContainer;
    [SerializeField] private Button openCloseButton;
    [SerializeField] private GameObject slotPrefab;

    [Header("Settings")]
    [SerializeField] private KeyCode toggleKey = KeyCode.I;
    [SerializeField] private bool startHidden = true;

    private InventorySystem inventorySystem;
    private List<InventorySlotUI> slots = new List<InventorySlotUI>();
    [Header("Item Interaction")]
    [SerializeField] private GameObject itemActionPanel;
    [SerializeField] private Button useButton;
    [SerializeField] private Button combineButton;
    [SerializeField] private Button examineButton;
    [Header("Description Panel")]
    [SerializeField] private GameObject descriptionPanel;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private Image descriptionImage;
    private InventorySlotUI selectedSlot;

    private void Awake()
    {
        // Получаем ссылку на систему инвентаря
        if (InventorySystem.current == null)
        {
            Debug.LogError("InventorySystem reference is missing!");
            return;
        }
        inventorySystem = InventorySystem.current;
        DontDestroyOnLoad(inventory);
        // Настраиваем кнопку
        openCloseButton.onClick.AddListener(ToggleInventory);
        // Скрываем инвентарь при старте если нужно
        if (startHidden)
        {
            inventoryPanel.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            UpdateInventoryDisplay();
        }
        // Инициализация панели действий
        itemActionPanel.SetActive(false);

        useButton.onClick.AddListener(OnUseClicked);
        combineButton.onClick.AddListener(OnCombineClicked);
        examineButton.onClick.AddListener(OnExamineClicked);
        if (descriptionPanel == null)
        Debug.LogError("Description Panel is not assigned!");
        if (titleText == null)
            Debug.LogError("Title Text is not assigned!");
        if (descriptionText == null)
            Debug.LogError("Description Text is not assigned!");
        if (descriptionImage == null)
            Debug.LogError("Description Image is not assigned!");
        
        if (descriptionPanel != null)
            descriptionPanel.SetActive(false);
    }

    private void Update()
    {
        // Открытие/закрытие по клавише
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleInventory();
        }
        // if (itemActionPanel.activeSelf && Input.GetMouseButtonDown(0))
    // {
    //     if (!RectTransformUtility.RectangleContainsScreenPoint(
    //         itemActionPanel.GetComponent<RectTransform>(), 
    //         Input.mousePosition))
    //     {
    //         itemActionPanel.SetActive(false);
    //     }
    // }
    }

    public void ToggleInventory()
    {
        bool newState = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(newState);
        PlayerController.IsTalking = newState;
        ConversationStarter.IsInv = newState;
        UseOnItem.IsUse = !newState;
        if (newState)
        {
            UpdateInventoryDisplay();
        }
    }

    private void UpdateInventoryDisplay()
    {
        if (inventorySystem == null)
        {
            Debug.LogError("InventorySystem reference is null!");
            return;
        }

        Debug.Log($"Updating inventory display. InventorySystem: {inventorySystem.name}");
        Debug.Log($"Item count: {inventorySystem.inventory.Count}");
        
        ClearSlots();

        foreach (InventoryItem item in inventorySystem.inventory)
        {
            if (item.data == null)
            {
                Debug.LogError("Found item with null data!");
                continue;
            }
            Debug.Log($"Creating slot for: {item.data.displayName} (x{item.stackSize})");
            CreateSlot(item);
        }
    }
    private void OnEnable()
    {
        if (inventorySystem != null)
            inventorySystem.OnInventoryChanged += UpdateInventoryDisplay;
    }

    private void OnDisable()
    {
        if (inventorySystem != null)
            inventorySystem.OnInventoryChanged -= UpdateInventoryDisplay;
    }
private void CreateSlot(InventoryItem item)
{
    // 1. Проверка входных данных
    if (item == null)
    {
        Debug.LogError("Item is null!");
        return;
    }
    if (item.data == null)
    {
        Debug.LogError($"Item data is null for item: {item}");
        return;
    }

    // 2. Проверка префаба
    if (slotPrefab == null)
    {
        Debug.LogError("Slot prefab is not assigned in inspector!");
        return;
    }

    // 3. Проверка контейнера
    if (slotsContainer == null)
    {
        Debug.LogError("Slots container is not assigned in inspector!");
        return;
    }

    // 4. Создание объекта
    GameObject slotObj;
    try
    {
        slotObj = Instantiate(slotPrefab, slotsContainer);
    }
    catch (Exception e)
    {
        Debug.LogError($"Failed to instantiate slot: {e.Message}");
        return;
    }

    // 5. Проверка компонента
    InventorySlotUI slot = slotObj.GetComponent<InventorySlotUI>();
    if (slot == null)
    {
        Debug.LogError($"Slot prefab '{slotPrefab.name}' is missing InventorySlotUI component!");
        Destroy(slotObj);
        return;
    }

    // 6. Инициализация
    try
    {
        slot.Initialize(item, OnSlotClicked);
        slots.Add(slot);
        Debug.Log($"Successfully created slot for {item.data.displayName} (Stack: {item.stackSize})");
    }
    catch (Exception e)
    {
        Debug.LogError($"Failed to initialize slot: {e.Message}");
        Destroy(slotObj);
    }
}
    private void ClearSlots()
    {
        foreach (InventorySlotUI slot in slots)
        {
            Destroy(slot.gameObject);
        }
        slots.Clear();
    }
public void OnSlotClicked(InventorySlotUI slot)
{
   
    if (slot == null || slot.Item == null || slot.Item.data == null)
        {
            Debug.LogError("Invalid slot or item data!");
            return;
        }

    selectedSlot = slot;
    InventoryItemData itemData = slot.Item.data;
    
    // Проверяем панель действий
    if (itemActionPanel == null)
    {
        Debug.LogError("Item Action Panel is not assigned!");
        return;
    }

    // Проверяем кнопки
    if (useButton == null || combineButton == null || examineButton == null)
    {
        Debug.LogError("Action buttons are not assigned!");
        return;
    }
    
    // Обновляем UI
    useButton.gameObject.SetActive(itemData.canBeUsed);
    combineButton.gameObject.SetActive(itemData.canBeCombined);
    examineButton.gameObject.SetActive(itemData.canBeExamined);


    itemActionPanel.SetActive(true);

    // Воспроизведение звука с проверкой
    if (itemData.interactionSound != null && Camera.main != null)
    {
        AudioSource.PlayClipAtPoint(itemData.interactionSound, Camera.main.transform.position);
    }
}
    public void OnUseClicked()
    {
        if (selectedSlot != null)
        {
            Debug.Log($"Using item: {selectedSlot.Item.data.displayName}");

            itemActionPanel.SetActive(false);
            UseOnItem.IsUse = true;
        }
        ToggleInventory();
        

    }

    public void OnCombineClicked()
{
    if (selectedSlot == null || selectedSlot.Item == null || selectedSlot.Item.data == null)
    {
        Debug.LogError("Invalid selected slot when trying to combine!");
        return;
    }

    // Скрываем панель действий
    itemActionPanel.SetActive(false);
    
    // Проверяем, можно ли вообще комбинировать этот предмет
    if (!selectedSlot.Item.data.canBeCombined)
    {
        ShowNotification("Этот предмет нельзя комбинировать");
        return;
    }

    // Включаем режим выбора второго предмета для комбинации
    StartCoroutine(CombineItemRoutine(selectedSlot.Item));
}

private IEnumerator CombineItemRoutine(InventoryItem firstItem)
{
    // Показываем сообщение о выборе второго предмета
    ShowNotification("Выберите предмет для комбинирования...");

    bool itemSelected = false;
    InventorySlotUI secondSlot = null;

    // Ждем выбора второго предмета
    while (!itemSelected)
    {
        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            // Проверяем, кликнули ли по слоту
            var raycastResults = new List<RaycastResult>();
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            
            EventSystem.current.RaycastAll(pointerEventData, raycastResults);

            foreach (var result in raycastResults)
            {
                secondSlot = result.gameObject.GetComponent<InventorySlotUI>();
                if (secondSlot != null && secondSlot.Item != null) break;
            }

            if (secondSlot != null && secondSlot.Item != null)
            {
                itemSelected = true;
            }
        }
        yield return null;
    }

    // Проверяем, можно ли объединить эти предметы
    if (CanCombineItems(firstItem.data, secondSlot.Item.data))
    {
        // Удаляем оба предмета из инвентаря
        inventorySystem.Remove(firstItem.data);
        inventorySystem.Remove(secondSlot.Item.data);

        // Добавляем результат комбинации
        inventorySystem.Add(firstItem.data.combinationResult);
        
        ShowNotification($"Получен: {firstItem.data.combinationResult.displayName}");
    }
    else
    {
        ShowNotification("Эти предметы нельзя объединить");
    }
}

private bool CanCombineItems(InventoryItemData firstItem, InventoryItemData secondItem)
{
    // Проверяем, есть ли второй предмет в списке combinableWith первого
    foreach (string combinableId in firstItem.combinableWith)
    {
        if (combinableId == secondItem.id)
        {
            return true;
        }
    }

    // Проверяем наоборот (если комбинация двунаправленная)
    foreach (string combinableId in secondItem.combinableWith)
    {
        if (combinableId == firstItem.id)
        {
            return true;
        }
    }

    return false;
}

private void ShowNotification(string message)
{
    // Реализуйте этот метод для показа сообщений игроку
    // Например, через всплывающую панель или UI Text
    Debug.Log(message);
    
    // Пример реализации через UI:
    // notificationText.text = message;
    // notificationPanel.SetActive(true);
    // StartCoroutine(HideNotificationAfterDelay(2f));
}

public void OnExamineClicked()
{
    Debug.Log($"Examining: {selectedSlot.Item.data.displayName}");
    if (selectedSlot != null && selectedSlot.Item != null)
        {
            Debug.Log($"Examining: {selectedSlot.Item.data.displayName}");
            ShowDescriptionPanel(selectedSlot.Item.data);
            itemActionPanel.SetActive(false);

            // Дополнительные эффекты
            Debug.Log($"Examining: {selectedSlot.Item.data.displayName}");
            
        }
}
    private void ShowDescriptionPanel(InventoryItemData itemData)
    {
        if (descriptionPanel == null)
        {
            Debug.LogError("Cannot show description - panel is null!");
            return;
        }

        // Заполняем данные
        titleText.text = itemData.displayName;
        descriptionText.text = itemData.examineDescription;
        descriptionImage.sprite = itemData.icon;

        // // Позиционируем панель
        // descriptionPanel.transform.position = Input.mousePosition + new Vector3(50, -50, 0);

        // Активируем
        descriptionPanel.SetActive(true);
        Debug.Log($"Showing description for: {itemData.displayName}");
    }
public void HideDescriptionPanel()
{
    if (descriptionPanel != null)
    {
        descriptionPanel.SetActive(false);
        Debug.Log("Description panel hidden");
    }
}

// Для кнопки закрытия
public void OnCloseDescription()
{
    HideDescriptionPanel();
}


   
}
