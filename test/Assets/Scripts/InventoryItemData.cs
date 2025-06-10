using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]
public class InventoryItemData : ScriptableObject
{
    [Header("Basic Info")]
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;
    
    [Header("Item Actions")]
    [Tooltip("Можно ли использовать этот предмет")]
    public bool canBeUsed = false;
    
    [Tooltip("Можно ли комбинировать этот предмет с другими")]
    public bool canBeCombined = false;
    
    [Tooltip("Можно ли осматривать этот предмет")]
    public bool canBeExamined = true;
    
    [Header("Usage Settings")]
    [Tooltip("Максимальное количество использований (0 = бесконечно)")]
    public int maxUses = 1;
    
    [Tooltip("Текущее количество оставшихся использований (не редактировать вручную)")]
    public int remainingUses;
    
    [Header("Effects System")]
    [Tooltip("Список эффектов при использовании")]
    public ItemEffect[] useEffects;
    
    [TextArea(3, 10)]
    [Tooltip("Описание при изучении предмета")]
    public string examineDescription;
    
    [Header("Combine Settings")]
    [Tooltip("ID предметов, с которыми можно комбинировать")]
    public string[] combinableWith;
    
    [Tooltip("Результирующий предмет после комбинации")]
    public InventoryItemData combinationResult;
    
    [Header("UI Settings")]
    [Tooltip("Цвет фона для слота с этим предметом")]
    public Color slotBackgroundColor = Color.clear;
    
    [Tooltip("Дополнительный звук при взаимодействии")]
    public AudioClip interactionSound;

    // Инициализация количества использований
    private void OnEnable()
    {
        if (remainingUses == 0 && maxUses > 0)
        {
            remainingUses = maxUses;
        }
    }
}

[System.Serializable]
public class ItemEffect
{
    [Tooltip("ID эффекта")]
    public string effectTarget;
    
    [Tooltip("Описание эффекта для отображения игроку")]
    public string effectDescription;
}