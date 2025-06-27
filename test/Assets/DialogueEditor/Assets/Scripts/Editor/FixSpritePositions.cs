using UnityEngine;
using UnityEditor;

public class FixSpritePositions : MonoBehaviour
{
    [MenuItem("Tools/Fix Sprite Positions After PPU Change")]
    public static void FixPositions()
    {
        foreach (SpriteRenderer spriteRenderer in FindObjectsOfType<SpriteRenderer>())
        {
            Sprite sprite = spriteRenderer.sprite;
            if (sprite == null) continue;

            // Если Pivot не в центре, вычисляем смещение
            Vector2 pivotOffset = sprite.pivot - sprite.rect.size * 0.5f;
            pivotOffset /= sprite.pixelsPerUnit; // Переводим в юниты

            // Применяем коррекцию позиции
            spriteRenderer.transform.position -= (Vector3)pivotOffset;
        }
        Debug.Log("Sprite positions fixed!");
    }
}