using UnityEngine;

public class BoolVariableChanger : MonoBehaviour
{
    [Tooltip("Имя переменной (должна быть статической в Suzhet)")]
    public string variableName;
    public bool newValue;

    public void ChangeBoolVariable()
    {
        var field = typeof(Suzhet).GetField(variableName);
        if (field != null && field.FieldType == typeof(bool))
        {
            field.SetValue(null, newValue);
            Debug.Log($"{variableName} изменено на: {newValue}");
        }
        else
        {
            Debug.LogError($"Переменная {variableName} не найдена или не является bool!");
        }
    }
}