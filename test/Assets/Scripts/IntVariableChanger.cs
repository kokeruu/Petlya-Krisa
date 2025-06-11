using UnityEngine;

public class IntVariableChanger : MonoBehaviour
{
    [Tooltip("Имя переменной (должна быть статической в Suzhet)")]
    public string variableName;
    public int newValue;

    public void ChangeIntVariable()
    {
        var field = typeof(Suzhet).GetField(variableName);
        if (field != null && field.FieldType == typeof(int))
        {
            field.SetValue(null, newValue);
            Debug.Log($"{variableName} изменено на: {newValue}");
        }
        else
        {
            Debug.LogError($"Переменная {variableName} не найдена или не является int!");
        }
    }
}