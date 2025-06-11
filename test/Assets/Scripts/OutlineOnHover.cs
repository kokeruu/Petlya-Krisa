using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class OutlineOnHover : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    
    [SerializeField] private Color hoverTint = new Color(0.8f, 0.3f, 0.3f, 1f); // Бордовый оттенок
    [SerializeField] private float tintIntensity = 0.5f; // Интенсивность эффекта (0-1)

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        // Применяем tint, смешивая оригинальный цвет с hoverTint
        spriteRenderer.color = Color.Lerp(originalColor, 
                                        new Color(
                                            originalColor.r * hoverTint.r,
                                            originalColor.g * hoverTint.g,
                                            originalColor.b * hoverTint.b,
                                            originalColor.a),
                                        tintIntensity);
    }

    private void OnMouseExit()
    {
        // Восстанавливаем оригинальный цвет
        spriteRenderer.color = originalColor;
    }
}